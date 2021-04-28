using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApertureScience.AccelerometerIngest.Domain.Entities
{
    [Table("Accelerometer", Schema = "Accelerometer")]
    public class Ingest
    {

        [Key]
        public Guid Id { get; private set; }
        [Required]
        public Guid UserId { get; private set; }
        [Required]
        public int DimensionX { get; private set; }
        [Required]
        public int DimensionY { get; private set; }
        [Required]
        public int DimensionZ { get; private set; }
        [Required]
        public long TimeStamp { get; private set; }



        private Ingest()
        {
            // for ef
        }
        public Ingest(Guid id , Guid userId,int dimensionX,int dimensionY, int dimensionZ,long timeStamp)
        {
            Id = id;
            UserId = userId;
            DimensionX = dimensionX;
            DimensionY = dimensionY;
            DimensionZ = dimensionZ;
            TimeStamp = timeStamp;
        }
    }
}
