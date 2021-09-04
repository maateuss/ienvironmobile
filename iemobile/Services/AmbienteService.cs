using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Interfaces;
using iemobile.Models;

namespace iemobile.Services
{
    public class AmbienteService : IAmbienteService
    {

        public async Task<IEnumerable<Ambiente>> BuscarAmbientes()
        {
            await Task.Delay(15);
            string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam eros libero, faucibus a cursus at, suscipit ullamcorper tortor. Aenean vestibulum, nisl a tempor aliquet, dolor leo aliquam ipsum, tempus scelerisque orci urna in urna. Nunc sit amet arcu arcu. Etiam sit amet leo in enim mattis dictum sit amet ac lectus. Sed pulvinar id eros nec fringilla. Praesent eu arcu odio. Curabitur ut leo massa. Morbi sit amet magna massa. Nulla facilisi. Aenean finibus metus eu pretium tincidunt. Nunc tincidunt risus sed purus varius commodo. Vestibulum nec odio felis. Duis ligula justo, semper ut facilisis in, scelerisque id tortor. Quisque feugiat, nisi sit amet cursus rutrum, justo magna congue tortor, et finibus ipsum ex non dui.";
            return new List<Ambiente>
            {
                new Ambiente { ImageSource = "https://picsum.photos/200", NomeAmbiente = "Sala Fênix", DescricaoAmbiente = LoremIpsum },
                new Ambiente { ImageSource = "https://picsum.photos/200", NomeAmbiente = "Sala da Justiça", DescricaoAmbiente = LoremIpsum },
                new Ambiente { ImageSource = "https://picsum.photos/200", NomeAmbiente = "Sala Vermelha", DescricaoAmbiente = LoremIpsum },
                new Ambiente { ImageSource = "https://picsum.photos/200", NomeAmbiente = "Escritório Principal", DescricaoAmbiente = LoremIpsum },
                new Ambiente { ImageSource = "https://picsum.photos/200", NomeAmbiente = "Terraço", DescricaoAmbiente = LoremIpsum },
                new Ambiente { ImageSource = "https://picsum.photos/200", NomeAmbiente = "Sala Central", DescricaoAmbiente = LoremIpsum },
                new Ambiente { ImageSource = "https://picsum.photos/200", NomeAmbiente = "Pátio Superior", DescricaoAmbiente = LoremIpsum },
                new Ambiente { ImageSource = "https://picsum.photos/200", NomeAmbiente = "Quarto Branco", DescricaoAmbiente = LoremIpsum }
            };
        }
    }
}
