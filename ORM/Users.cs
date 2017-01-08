namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Blogs = new HashSet<Blogs>();
            Comments = new HashSet<Comments>();
            Articles = new HashSet<Articles>();
            Blogs1 = new HashSet<Blogs>();
            Comments1 = new HashSet<Comments>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string Login { get; set; }

        [Required]
        [StringLength(30)]
        public string Firstname { get; set; }

        [StringLength(30)]
        public string Secondname { get; set; }

        [StringLength(30)]
        public string Thirdname { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime DateRegistered { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        public int RoleId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blogs> Blogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }

        public virtual Roles Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articles> Articles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blogs> Blogs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments1 { get; set; }
    }
}
