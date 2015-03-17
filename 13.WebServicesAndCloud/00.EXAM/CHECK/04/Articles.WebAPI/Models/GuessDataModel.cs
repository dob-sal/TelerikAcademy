using Articles.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
namespace Articles.WebAPI.Models
{
    public class GuessDataModel
    {
        public GuessDataModel() { }

        public static Expression<Func<Guess, GuessDataModel>> FromGuess
        {
            get
            {
                return game => new GuessDataModel
                {
                    ID = game.ID,
                    
                };
            }
        }

        public int ID { get; set; }
    }
}