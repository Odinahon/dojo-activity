using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class User: BaseEntity
    {
        [Key]
        public int UserId {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public List <Activity> ActivityCreated {get;set;}
        public List <ActivityUser> ActivityJoin {get;set;}

        public User ()
        {
            ActivityCreated = new List<Activity>();
            ActivityJoin = new List<ActivityUser>();
            
        }
    }
}