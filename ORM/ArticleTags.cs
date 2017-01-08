namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArticleTags
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int ArticleId { get; set; }

        public int TagId { get; set; }

        public virtual Articles Articles { get; set; }

        public virtual Tags Tags { get; set; }
    }
}
