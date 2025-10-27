#  FolhaEsig API

API desenvolvida em *ASP.NET Core* com *Entity Framework Core* e *MySQL*.  
Este projeto faz parte do sistema **FolhaEsig**, responsável por gerenciar pessoas, cargos e salários.


##  Pré-requisitos

Antes de rodar a API, certifique-se de ter instalado:

- [.NET SDK 8.0 ou superior](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [MySQL Workbench](https://dev.mysql.com/downloads/workbench/)



## 🗄️ Configurar o banco de dados

### 1️⃣ Crie o banco de dados e as tabelas

Use o arquivo:

sql_criar_entidades_procedures_views.sql

Esse arquivo contém **todo o script** para criar:
- O banco **`FolhaEsigDB`**
- Tabelas: `cargo`, `pessoa`, `pessoa_salario`
- Views: `vw_listarpessoas`, `vw_listarcargos`
- Procedures: `InserirCargo`, `InserirPessoa`, `AtualizarPessoaSalario`, `ListarPessoaSalario`, `RemoverPessoa`

**Como executar o script:**
1. Abra o **MySQL Workbench**.  
2. Clique em **File → Open SQL Script**.  
3. Selecione `sql_criar_entidades_procedures_views.sql`.  
4. Clique em *Execute*.

---

##  Configurar a conexão com o banco

No arquivo `appsettings.json`, verifique se a string de conexão está assim:

```json

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3307;Database=FolhaEsigDB;Uid=root;Pwd=1923;"
}
```
###  Executar a API

1. Abra o terminal na pasta do projeto (onde está o arquivo `FolhaEsigAPI.sln`).

2. Acesse a subpasta onde está o arquivo `.csproj`:
   ```bash
   cd folhaEsigApi
   ```

Execute o comando:

dotnet run

Espere a mensagem no terminal indicando que a aplicação foi iniciada, algo como:

Now listening on: http://localhost:5000
Now listening on: https://localhost:5001

### Acessar o Swagger

Com a API rodando, abra o navegador e acesse:

🔗 Swagger (HTTP):
http://localhost:5000/swagger

🔐 Swagger (HTTPS):
https://localhost:5001/swagger
