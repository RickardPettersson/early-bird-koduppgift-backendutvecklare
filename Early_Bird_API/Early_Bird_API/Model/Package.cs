namespace Early_Bird_API.Model
{
    public class Package
    {
        public string KolliId { get; set; }
        
        /// <summary>
        /// Saved in "gram"
        /// </summary>
        public uint Weight { get; set; }
        
        public uint Length { get; set; }
        
        public uint Height { get; set; }
        
        public uint Width { get; set; }
        
        public bool IsValid { get
            {
                // Check if package weight is over 20 kilogram, the value Weight is saved in gram
                if (Weight > Misc.ValidationHelper.MaxWeight) return false;

                if (Length > Misc.ValidationHelper.MaxLength) return false;
                
                if (Height > Misc.ValidationHelper.MaxHeight) return false;
                
                if (Width > Misc.ValidationHelper.MaxWidth) return false;
                
                return true;
            }
        }   
    }
}
