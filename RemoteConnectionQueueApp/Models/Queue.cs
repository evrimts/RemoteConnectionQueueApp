using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteConnectionQueueApp.Models
{
    public class Queue
    {
        [Key]
        public int Id { get; set; }

        public string BekleyenKisi { get; set; }

        public int RemoteConnectionId { get; set; }

        [ForeignKey("RemoteConnectionId")]
        public RemoteConnection RemoteConnection { get; set; }
    }
}
