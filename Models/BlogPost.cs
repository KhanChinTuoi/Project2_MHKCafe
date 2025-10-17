namespace MHKCafe.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public required string TieuDe { get; set; }
        public required string MoTa { get; set; }
        public required string NoiDung { get; set; }
        public required string HinhAnh { get; set; }
        public required string NgayDang { get; set; }
    }
}
