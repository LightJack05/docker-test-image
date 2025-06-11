using System.ComponentModel.DataAnnotations;
namespace DockerTestImage.Models;

public class Counter
{
    [Key]
    public int State { get; set; } = 0;
}
