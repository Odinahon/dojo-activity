using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exam.Models;

namespace Exam
{
    public class ActivityUser
    {
        [Key]
        public int ActivityUserId {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public int ActivityId {get;set;}
        public Activity Activity{get;set;}
        
    }
}