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
