
## 🚀 Api Rest
Se trata de uma simples Api para fins de estudo de campeões baseados em League of Legends. 
A Api já inicia com alguns campeões cadastrados, mas é possível adicionar mais campeões.

Para utiliza-la é necessário que o utilizador realize o login pelo método de post em Login.

| Usuário | Senha |
|---------|------|
| admin@gft.com | senhaforte 

É possível se registrar com novos logins.


## Rodar Aplicação

Clone o projeto

```bash
  git clone https://github.com/2dsant/LeagueOfLegends_API.git
```

Abra o diretório

```bash
  cd LeagueOfLegends_API
```

Configurar o arquivo appsetting.json de acordo com as configurações do seu banco mysql.
No caso a minha configuração é essa:
```bash
    "DefaultConnection": "server=localhost;port=3307;database=bd_leagueoflegends;uid=root;password=root"
```

Startar a aplicação
```bash
  dotnet watch run
```



Api documentada via Swagger assim que se inicia.


