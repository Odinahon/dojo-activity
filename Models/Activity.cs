using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam.Models
{
    public class Activity: BaseEntity
    {
        [Key]
        public int ActivityId {get; set;}
        [Required]
        [MinLength(2)]
        public string Title {get;set;}
        [Required]
        [MinLength(10)]
        public string Description {get;set;}
        [Required]
        [CheckFuture]
        public DateTime Date{ get;set;}
        [Required]
        [CheckFuture]
        public DateTime Time{get;set;}
        public int Duration {get;set;}
        public string ActivityLength {get; set;}
        [ForeignKey ("WhoCreateThisActivity")]
        public int UserId {get; set;}
        public User WhoCreateThisActivity{get;set;}
        public List <ActivityUser> Users{get;set;}
        public Activity()
        {
            Users = new List<ActivityUser>();
        }
        

    }
}