//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Restaurants
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Restaurants()
        {
            this.Reserves = new HashSet<Reserves>();
        }

        [Key]
        [DisplayName("餐廳編號")]
        public int RestaurantID { get; set; }

        [DisplayName("餐廳名稱")]
        [Required(ErrorMessage = "請填寫餐廳名稱")]
        [StringLength(30, ErrorMessage = "餐廳名稱不得超過50字")]
        public string Name { get; set; }

        [DisplayName("餐廳電話")]
        [StringLength(12, ErrorMessage = "餐廳電話不得超過12字")]
        public string ContactNumber { get; set; }

        [DisplayName("地址")]
        [Required(ErrorMessage = "請填寫餐廳地址")]
        [StringLength(100, ErrorMessage = "餐廳地址不得超過100字")]
        public string Address { get; set; }

        [DisplayName("營業時間")]
        [StringLength(100, ErrorMessage = "營業時間不得超過100字")]
        public string OpeningHours { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reserves> Reserves { get; set; }
    }
}
