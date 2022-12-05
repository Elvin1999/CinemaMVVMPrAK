using CinemaMVVM.Commands;
using CinemaMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CinemaMVVM.ViewModels
{
    public class MovieCellViewModel : BaseViewModel
    {
        public WrapPanel StarsPanel { get; set; }
        private Movie movie;
        public Movie Movie
        {
            get { return movie; }
            set
            {
                movie = value;
                if (movie.Description.Length >= 23)
                    movie.Description = movie.Description.Substring(0, 23);

                if (movie.Name.Length >= 18)
                    movie.Name = movie.Name.Substring(0, 18);

                OnPropertyChanged();
            }
        }
        private CustomImageModel model;

        public CustomImageModel Model
        {
            get { return model; }
            set { model = value; OnPropertyChanged(); }
        }


        public RelayCommand SelfClickCommand { get; set; }


        public MovieCellViewModel()
        {
            SelfClickCommand = new RelayCommand((o) =>
            {
                Model.BackImagePath = Movie.ImagePath;
            });
        }
    }
}
