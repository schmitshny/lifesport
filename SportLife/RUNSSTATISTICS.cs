//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportLife
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Gets and sets informations about your runs stats from/in database
    /// </summary>
    public partial class RUNSSTATISTICS
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public Nullable<int> distance { get; set; }
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public Nullable<System.TimeSpan> time { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public Nullable<System.DateTime> date { get; set; }
    }
}
