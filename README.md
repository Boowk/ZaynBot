<p align="center"><a href="https://discordbots.org/bot/459873132975620134" >
  <img src="https://discordbots.org/api/widget/459873132975620134.svg" alt="ZaynRPG" />
</a><br>
  <a href="https://discordbots.org/bot/459873132975620134" >
  <img src="https://discordbots.org/api/widget/status/459873132975620134.svg" alt="ZaynRPG" />
</a></p>
Olá, eu me chamo Zayn e sou apenas um simples bot brasileiro para o Discord com várias funções jamais vistas!

Eu tenho várias funções para entretenimento (como comandos engraçados e memes), funções sociais (sistema de perfil com XP e reputação ainda em desenvolvimento) e como foco principal ser o melhor bot brasileiro de RPG do discord!

Um dos motivos que me fizeram me tornar o que eu sou hoje é a falta de bots brasileiros para o Discord, já que muitos servidores brasileiros no Discord usam bots "de baixa qualidade" em português ou usam bots que falam (na verdade escrevem) em inglês... e convenhamos, nenhuma dessas opções parece agradável para os usuários... e por isto que eu prometo mudar isto!

## 🤔 Como adiciono ela ao meu servidor?

Se você quiser usar a Zaynrina no seu servidor, você pode adicionar ela clicando [aqui](https://discordapp.com/api/oauth2/authorize?client_id=459873132975620134&permissions=469887175&scope=bot).

Enquanto é possível fazer "self hosting" (hospedar você mesmo) ela, eu não irei dar suporte para quem quiser fazer isto para evitar pessoas criando "clones" e levando todo o crédito por terem criado o bot, eu dou suporte se você quer fazer "self hosting" para ajudar e contribuir para ela.

## 💁 Suporte

Você pode obter suporte sobre a Zaynrina [clicando aqui](https://discord.gg/GGRnMQu)!

## 🙋 Como ajudar?

Basta fazer um clone nesse repositorio.
Ter instalado MongoDB e o Visual Studio 2018 para cima com .Net Core instalado.
Crie um comando, melhore algo, corrija algo. E faça um pull request.
Você também pode abrir issues.

### 💵 Como Doar?

Por enquanto não criei nenhuma forma de doação, futuramente terá

### 🙌 Como Usar?
#### 👨‍💻 Como Compilar?

Você também pode hospedar a Zaynrina em algum lugar se você não quiser utilizar a versão pública dela, mas lembrando...
* Nós deixamos o código-fonte de nossos projetos para que outras pessoas possam se inspirar e aprender com nossos projetos, o objetivo é que pessoas que são fãs da Zaynrina aprendam como ela funciona e, caso queiram, podem ajudar ela com bug fixes e novas funcionalidades.
* Eu não irei dar suporte caso você queria fazer self hosting apenas para você fazer "fama" falando que você criou um bot, mesmo que na verdade você apenas pegou o código-fonte dela e hospedou, lembre-se, a licença do projeto é [AGPL v3](https://github.com/ZaynBot/ZaynBot/blob/master/LICENSE), você é **obrigado a deixar todas as suas alterações no projeto público**!
* Eu não irei ficar explicando como arrumar problemas na sua versão self hosted dela, **você está por sua conta e risco**.
* Eu irei dar suporte caso você queria hospedar ela para contribuir e ajudar ela.
* Lembrando que ela precisa de várias API Keys para várias funcionalidades dela, caso você não coloque uma delas, talvez ela poderá ter funcionalidade reduzida ou talvez não irá funcionar corretamente!
* Lembrando que eu não distribuo os "assets" dela (imagens, fontes, etc), ou seja, comandos que utilizam tais assets não irão funcionar corretamente.
* Existem várias coisas "hard coded" nela, ou seja, você terá que editar o código-fonte dela e recompilar, afinal, eu nunca pensei que alguém ia usar hospedar a Zayn  então você terá que fazer algumas modificações no código-fonte dela para funcionar. 😉
* Eu hospedo ela em uma máquina rodando Ubuntu 16.4, talvez ela não irá rodar corretamente em outros sistemas operacionais.
* Você não pode utilizar o nome "Zayn" na sua versão self hosted.

Mas se você quiser mesmo hospedar a Zaynrina, siga os seguintes passos:
1. Tenha o MongoDB instalado na sua máquina.
2. Tenha o Visual Studio 2018 (ou superior) na sua máquina com .Net Core 2.1.
3. Tenha o Git Bash instalado na sua máquina.
4. Faça ```git clone https://github.com/ZaynBot/ZaynBot.git``` em alguma pasta no seu computador.
5. Agora, usando o PowerShell (ou o próprio Git Bash), entre na pasta criada e utilize `dotnet publish -c Release -r win10-x64`
6. Após terminar de compilar, vá na pasta `release` e execute o .EXE
7. Após iniciar, um arquivo chamado `config.json` será criado, abra ele com um editor de texto decente (como o Notepad++) e preencha todas as opções, a configuração já vem com alguns valores padrões e alguns destes valores padrões vem com explicações sobre para que ele serve e da onde ele surgiu.
8. Após terminar de configurar, inicie o .EXE novamente e, se tudo der certo, ela irá iniciar e você poderá usar os comandos dela! 🎉

#### 🔀 Pull Requests

No seu Pull Request, você deverá seguir o meu estilo de código bonitinho que eu faço, é recomendado que você coloque comentários nas partes do seu código para que seja mais fácil na hora da leitura.

O seu código não pode ser algo "gambiarra", meu código pode ter algumas gambiarras mas isto não significa que você também deve encher a Zayn com mais gambiarras no seu Pull Request.

Você precisa pensar "será que alguém iria utilizar isto?", se você criar um comando que só seja útil no seu servidor, provavelmente eu irei negar o seu Pull Request.

Funcionalidades (como comandos) relacionados a coisas NSFW **não serão** adicionadas na Zayn e seu Pull Request será negado, eu prefiro que as pessoas, ao olharem o avatar dela, pensem que ela é um bot que cria memes toscos do que um bot que fica mandando coisas NSFW no chat.

## 📦 Dependências

A Zaynrina utiliza várias [dependências no código-fonte dela](https://github.com/ZaynBot/ZaynBot/blob/master/ZaynBot/ZaynBot.csproj), obrigado a todos os mantenedores das dependências! Sem vocês, talvez a Zaynrina não iria existir (ou teria várias funcionalidades reduzidas ou talvez até inexistentes!)

| Nome  | Mantenedor |
| ------------- | ------------- |
| [DsharpPlus](https://github.com/DSharpPlus/DSharpPlus) | Emzi0767, NaamloosDT  |
| [DSharpPlus.CommandsNext](https://github.com/DSharpPlus/DSharpPlus) | Emzi0767, NaamloosDT  |
| [DSharpPlus.Interactivity](https://github.com/DSharpPlus/DSharpPlus) | Emzi0767, NaamloosDT  |
| [DSharpPlus.Rest](https://github.com/DSharpPlus/DSharpPlus) | Emzi0767, NaamloosDT  |
| [DSharpPlus.VoiceNext](https://github.com/DV8FromTheWorld/JDA) | Emzi0767, NaamloosDT  |
| [MongoDB.Driver](https://github.com/mongodb/mongo-csharp-driver) |  Vincent Kam, Dmitry Lukyanov, Robert Stam, Craig Wilson etc  |

## 💫 Agradecimentos especiais para...

Imain#5986 do Discord por ter me dado motivação para continuar com o Bot.

Fusion_#1609 do Discord por ter colocado o bot no Server dele, onde encontrei mais pessoas que queria a continuação.

𝓚α𝓇η#6775 do Discord por ter ajudado nas ideias do RPG

## 📄 Licença

O código-fonte da Zaynrina está licenciado sob a [GNU Affero General Public License v3.0](https://github.com/ZaynBot/ZaynBot/blob/master/LICENSE)

Ao utilizar a Zayn você aceita os termos de uso dela que são
1. Não usaras outro bot para automatizar comandos.
2. Não abusaras de bug para beneficio proprio.
3. Reportara todos os bugs encontrado no canal oficial do discord

<hr>
<br>
<p align="center">"Discord", "DiscordApp" and any associated logos are registered trademarks of Discord Inc.</p>
