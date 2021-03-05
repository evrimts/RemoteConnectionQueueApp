using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteConnectionQueueApp.Models
{
    public class RemoteConnection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Müşteri Adı")]
        public string MusteriAdi { get; set; }

        [Display(Name = "Sunucu IP")]
        [StringLength(60)]
        public string SunucuIp { get; set; }

        [Display(Name = "Sunucu Kullanıcı Adı")]
        [StringLength(60)]
        public string SunucuKullaniciAdi { get; set; }
        
        [Display(Name = "Bağlı Kişi")]
        [StringLength(120)]
        public string BagliKisi { get; set; }

        [Display(Name = "Bağlantı Zamanı")]
        public DateTime? BaglantiZamani { get; set; }

        [StringLength(255)]
        [Display(Name = "Not")]
        public string Note { get; set; }
    }
}
