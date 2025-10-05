namespace MenuDigitalApi.DTOs.Menu.Products.Request.Update
{
    public class AdditionalUpdateDto
    {
        public string? Category { get; set; }

        public string? Name { get; set; }
        public string? Size { get; set; }

        public int? Min { get; set; } = 0;

        public int? Max { get; set; } = 0;

        public string[]? ProductIdList { get; set; }
    }
}
