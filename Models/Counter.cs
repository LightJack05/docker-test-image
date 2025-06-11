using System.ComponentModel.DataAnnotations;
namespace DockerTestImage.Models;

public class Counter
{
    [Key]
    public int Id { get; set; } = 0;
    public int State { get; set; } = 0;
}
