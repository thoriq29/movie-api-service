using Movie.Common.Models.Movie;
using Service.Core.MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Common.Models.Genre
{
    public class GenreModel : BaseModel
    {
        [Required]
        [Column("name")]
        public string Name { get; set; }
        
        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted()
        {
            return DeletedDate.HasValue;
        }

        public ICollection<MovieModel> Movies { get; set; }
    }
}
