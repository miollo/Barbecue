# Barbecue

Desenvolvimento de uma api em dotnet 3.1 para gerenciamento de churrascos.

# Configurações

Para rodar a api entrar na pasta principal e rodar
> dotnet run 

 Criar um banco de dados [PostgresSQL](https://www.postgresql.org/download/).

Renomear o arquivo 'appsettings.example.json' para 'appsettings.json' e alterar as informações da linha que contém BarbecueConnection (11) para as informações do seu banco de dados (host - porta - base - usuário - senha)

Executar a migrations com o comando abaixo na pasta principal após ter o banco configurado.
> dotnet ef database update

# Endpoints

Para testar os endpoints foi utilizado o [Insomnia Core](https://insomnia.rest/download/). O arquivo '**Insomnia_Barbecue.json**', que pode ser encontrado no diretório raiz do backend, contém os endpoints corretos, sendo necessário modificar o corpo dos json e os ids quando necessário. Ele é subdivido em 2 partes. Devido a esse arquivo já possuir a estrutura dos endpoints e urls corretas de acesso, não repetirei essa parte aqui. apenas irei descreve-los brevemente. 

* Pessoas

Endpoints relacionados ao cadastro de pessoas, são eles:
1. **GetAllPeople:** retorna todas pessoas do banco.
2. **GetPerson:** retorna uma pessoa específica pelo id fornecido.
3. **CreatePerson:** cria uma pessoa com base nas informações do corpo do json.
4. **PutPerson:** atualiza as informações de uma pessoa específica por id.
5. **DeletePerson:** deleta uma pessoa específica por id.

* Churrasco

Endpoints relacionado ao cadastro de churrasco e ao adicionar pessoas nos churrascos, são eles:
1. **GetAllBarbecues:** retorna todos churrascos cadastrados, nesse endpoint não são retornadas informações sobre as pessoas presentes no churrasco.
2. **GetBarbecue:** retorna as informações de um churrasco específico por id e as pessoas presentes nele.
3. **DeleteBarbecue:** Deleta um churrasco específico por id e os vinculos com as pessoas.
4. **CreateBarbecue:** Cria um churrasco com base nas informações do corpo do json.
5. **UpdateBarbecue:** Atualiza um churrasco específico por id, nesse caso é possível não enviar todas informações que só serão atualizadas as preenchidas.
6. **AddPersonOnBarbecue:** Adiciona uma pessoa em um churrasco, e sinaliza se ela irá beber e se já pagou.
7. **UpdatePersonOnBarbecue:** Atualiza uma pessoa que está presente em um churrasco, o id do churrasco é passado pela url e o da pessoa pelo corpo do json, podem não ser mandadas todas informações que só serão atualizada as que forem enviadas.
8. **AddAllPeopleOnBarbecue:** Adiciona todas pessoas criadas em um churrasco específico por id.
9. **DeletePersonFromBarbecue:** Deleta uma pessoa específica por id de um churrasco específico por id, id do churrasco é passado na url e da pessoa no corpo do json.
