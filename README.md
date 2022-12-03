## Salvando imagem dentro do wwwroot

Esse repositório tem como finalidade de salvar imagem dentro da nossa pasta WWWROOT do projeto.
Para esse projeto utilizamos o ASP.NET Core 7.

> Lembrando que a ideia dessa aplicação é apenas salvar a imagem no wwwroot, então não é uma aplicação robusta e sim simplesmente para salvar nossas imagens.
Após criação do projeto, vamos criar uma classe chamada **PessoaModel** com as seguintes propriedades.

```csharp
    [Key]
    public int Id {get; set;}
    
    [MaxLength(50)]
    public string Nome {get; set;}
    
    public string Foto {get; set;}
    
```
    
Após a criação das propriedades acima, vamos criar um controller utilizando o item scaffold.

> Botão direito em cima da pasta Controller >> Adicionar >> Novo item com scaffold

![image](https://user-images.githubusercontent.com/99252640/205414533-6661e203-9ee1-49e8-8d45-532d6bfbd9a6.png)

Uma janela vai ser exibida clique no apção conforme abaixo.

![image](https://user-images.githubusercontent.com/99252640/205414578-277294ee-6d90-48fd-8c18-d89ba2b4b30b.png)

Agora que adicionamos vamos configurar o nosso item.

![image](https://user-images.githubusercontent.com/99252640/205414743-bdeb40fc-21ab-4cfd-906b-331eb0f5cd5f.png)

1- Selecionar a classe na qual usaremos as propriedades dentro da view.




2- Selecionar seu datacontext ou criar um _Lembrando que se você criar um novo, terá que configurar dentro do program a classe que faz a conexão e nos arquivos appsettings.json_

3- Definir o nome da controller.

Agora com nosso controller e view Pessoa Criada, a primeira coisa que vamos fazer é modificar algumas coisas na view de create.

![image](https://user-images.githubusercontent.com/99252640/205415363-5d721af5-8a7d-4d42-b2ee-e9e2030f4b06.png)

1- Adicionar dentro da tag form a propriedade **enctype="multipart/form-data**
2- Dentro da tag de input vamo definir o tipo como file e criar o name como anexo. **type="file" name="anexo"**

Depois de ter feito essas modificações, vamos até em nosso novo controller e adicionar em nossa injeção de dependência IWebHostEnvironment e passar via assinatura do construtor.

```csharp
        private readonly PessoaContext _context;
        private string _filePath;

        public PessoaController(PessoaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _filePath = env.WebRootPath;
        }
    
```

Vamos criar dois métodos para validar e salvar a imagem em si.

```csharp
        public bool ValidarImagem(IFormFile file)
        {
            switch (file.ContentType)
            {
                case "image/jpeg":
                    return true;

                case "image/bmp":
                    return true;

                case "image/gif":
                    return true;

                case "image/png":
                    return true;
                default:
                    return false;
                    break;
            }
        }
    
```

```csharp
        public string SalvarArquivo(IFormFile file)
        {
            var nome = Guid.NewGuid().ToString() + file.FileName;
            var filePath = _filePath + "\\fotos";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            using (var stream = System.IO.File.Create(filePath + "\\" + nome))
            {
                file.CopyToAsync(stream);
            }

            return nome;
        }
    
```

Agora para finalizar nosso código, vamos salvar nossa imagem no banco para isso vamos adicionar acrescentar os código conforme abaixo:

![image](https://user-images.githubusercontent.com/99252640/205415854-0ae8b287-ee59-416c-82c4-4947376b9ca1.png)

1- Vamos adicionar em nossa assinatura o objeto **IFormFile anexo**

2 - Criar uma variável do tipo var que vai infocar nosso método que criamos acima para tentar salvar o arquivo.

```csharp
    var nome = SalvarArquivo(anexo);    
```
3- Valida o anexo que foi enviado através da requisição.

4- Se nossa variável que criamos a imagem foi salva dentro do wwwroot então atribui o valor dentro da propriedade Foto.

5- Salva o arquivo salvo em nossa base de dados.

## trabalhando com Migration

Agora vamos criar nossa migration, para isso devemos adicionar seguite o comendo:

![image](https://user-images.githubusercontent.com/99252640/205416203-faa3aeba-4d50-4674-a40b-1c50cac46ad8.png)

Agora só executar a aplicação. 🤘🏻
