namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comments()
        {
            Users1 = new HashSet<Users>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Content { get; set; }

        public int Rating { get; set; }

        public DateTime DateAdded { get; set; }

        public int UserId { get; set; }

        public int ArticleId { get; set; }

        public virtual Articles Articles { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users1 { get; set; }
    }
}
