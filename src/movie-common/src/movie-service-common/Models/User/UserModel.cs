using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Movie.Common.Models.UserReview;
using Service.Core.MySql;

namespace Movie.Common.Models.User
{
    public class UserModel: BaseModel
    {
        [Required]
        [Column("account_id")]
        public string AccountId { get; set; }

        [Required]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted()
        {
            return DeletedDate.HasValue;
        }

        public ICollection<UserReviewModel> Reviews { get; set; }
    }
}
