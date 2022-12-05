using CinemaMVVM.Commands;
using CinemaMVVM.Models;
using CinemaMVVM.Services;
using CinemaMVVM.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CinemaMVVM.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; OnPropertyChanged(); }
        }

        private CustomImageModel model;

        public CustomImageModel Model
        {
            get { return model; }
            set { model = value; OnPropertyChanged(); }
        }

        public async void SetMovie(object p) {
            var panel = p as WrapPanel;
            panel.Children.Clear();

            var movies = await MovieService.GetMoviesAsync(SearchText);

            Model = new CustomImageModel
            {
                BackImagePath = movies.First().ImagePath
            };


            int x = 10;
            int y = 10;

            foreach (var m in movies)
            {
                var ucVM = new MovieCellViewModel
                {
                    Movie = m,
                    Model = this.Model
                };

                var d = double.Parse(m.Rating.Replace('.', ',')).ToString();
                double result = (double.Parse(d) + 1) / 2;
                int count = (int)result;

                var uc = new MovieCellUC(ucVM);

                for (int i = 0; i < count; i++)
                {
                    ucVM.StarsPanel.Children.Add(new StarUC());
                }

                uc.Margin = new System.Windows.Thickness(x, y, 0, 0);


                panel.Children.Add(uc);

            }
        } 
        public RelayCommand SearchCommand { get; set; }
        public MainViewModel()
        {
            Model=new CustomImageModel();
            Model.BackImagePath = @"\images\cine.jpg";


            SearchCommand = new RelayCommand((p) =>
            {
                SetMovie(p);
            });
        }
    }
}
