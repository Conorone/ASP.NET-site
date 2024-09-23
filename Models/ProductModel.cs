using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Models;

public class ProductModel {
    [DisplayName("ID Number")]
    public int ID { get; set; }

    [DisplayName("Product Name")]
    public string Name { get; set; }

    [DataType(DataType.Currency)]
    [DisplayName("Price")]
    public decimal Price { get; set; }

    [DisplayName("Description")]
    public string? Description { get; set; }

    public int Stock { get; set; }
    public IFormFile? ImageFile { get; set; }
    public byte[]? Image {get; set; }

    public ProductModel() {
        if (ImageFile != null) {
            ImageToBinary();
        }
    }

    public async void ImageToBinary() {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await ImageFile.CopyToAsync(memoryStream);
                Image = memoryStream.ToArray(); // Update image if a new one is uploaded
            }
        }
    }
}