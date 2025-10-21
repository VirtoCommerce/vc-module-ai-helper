using Newtonsoft.Json;

namespace VirtoCommerce.AiHelper.Core.Models;
public class AiProductContract
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public string[] Images { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
