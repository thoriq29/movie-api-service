using Movie.Api.Dto.Movie;
using Movie.Common.Models.Movie;
using Service.Core.MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  Movie.Api.Dto.Genre
{
    public class GenreDto
    {
        public long ID { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        #nullable enable
        public List<MovieDto>? Movies { get; set; }
    }
}
