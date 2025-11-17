# ☁️ Introdução a Cloud com Azure:
Este repositório contém o resumo das lições aprendidas e anotações importantes que fiz durante as aulas, especificadamente para o lab **Localizando Serviços por Categoria**, do curso de Introdução a Cloud com Azure da DIO (Digital Innovation One). Nele, compartilho os principais conceitos abordados e exemplos práticos pensando na minha principal área de desenvolvimento -> o Back-End. 

# I. Conceitos Iniciais:

**Computação em Nuvem:** A Computação em Nuvem é o fornecimento de serviços de computação pela Internet. Isso inclui servidores, armazenamento, bancos de dados, redes, software e muito mais. Com ela, é possível acessar esses serviços de qualquer lugar e a qualquer momento, desde que haja conexão à Internet.

**Relevância para desenvolvedores que buscam uma carreira sólida em Back-End:** Ela tem sido uma transformação fundamental no modo como as aplicações e serviços são desenvolvidos, escalados e mantidos. Para desenvolvedores que buscam especialização em Back-End, entender a infraestrutura e os conceitos de nuvem, como escalabilidade, elasticidade e segurança, é essencial.

*Exemplo Prático:*
- Imagine que você está construindo um sistema de e-commerce. Se você usar a nuvem, pode escalar seu banco de dados e servidores automaticamente para lidar com o aumento de acessos durante datas de vendas, como Black Friday.

# 😶‍🌫️ II. Entendendo os Modelos de Implantação:

Entender a diferença entre os modelos de nuvem define o nível de controle e a segurança de uma aplicação. Cada modelo atende a necessidades diferentes, e a escolha depende de fatores como custo, flexibilidade e requisitos de segurança.

*Exemplo prático:*
- Se você tem uma aplicação que lida com dados sensíveis de clientes e precisa de altos níveis de conformidade (como PCI DSS para pagamentos), a Nuvem Privada ou um modelo Híbrido pode ser o mais adequado.

| Modelo            | Definição                                                                 | Vantagens                                                   | Desvantagens                                              |
| :---------------- | :------------------------------------------------------------------------ | :---------------------------------------------------------- | :-------------------------------------------------------- |
| **Nuvem Pública** | Serviços oferecidos por terceiros (Microsoft Azure, AWS, Google Cloud) em hardware compartilhado, acessíveis via internet. | Custo-benefício (pague pelo uso), **alta escalabilidade**, sem manutenção de infraestrutura. | Menos controle sobre o hardware, preocupações de segurança/conformidade em casos muito específicos. |
| **Nuvem Privada** | Infraestrutura de nuvem dedicada a uma única organização, hospedada em um data center próprio ou em um fornecedor terceirizado. | **Controle total** sobre dados e segurança, ideal para requisitos regulatórios estritos. | Custo inicial e operacional alto, responsabilidade total pela manutenção. |
| **Nuvem Híbrida** | Combinação de ambientes de nuvem pública e privada, conectados por tecnologia que permite o compartilhamento de dados e aplicativos. | **Flexibilidade máxima** (use a Nuvem Pública para picos de demanda e a Privada para dados sensíveis). | Maior complexidade de gerenciamento e segurança. |

# 💭 III. Benefícios da Computação em Nuvem:

A Nuvem resolve problemas operacionais complexos, permitindo que você se concentre na lógica de negócio do seu código, sem se preocupar com a infraestrutura subjacente.

| Benefício        | Significado e Importância para o Back-End                                                             | Exemplos                                                                                   |
| :----------------| :-------------------------------------------------------------------------------------------------- | :------------------------------------------------------------------------------------------ |
| **1. Alta Disponibilidade** | Garante que seu serviço esteja **sempre acessível**, mesmo que uma zona de *data center* falhe. Essencial para aplicações críticas (ex: E-commerce, serviços financeiros). | Azure oferece **Zonas de Disponibilidade** para garantir que sua aplicação esteja sempre ativa, mesmo em caso de falhas de hardware ou manutenção programada. |
| **2. Escalabilidade** | Capacidade de **adicionar ou remover recursos** para atender a um **crescimento de demanda esperado**. Seu código precisa ser *stateless* (sem estado) para escalar facilmente. | A **Escalabilidade Automática** no Azure permite que você adicione ou remova instâncias de máquinas virtuais automaticamente conforme a demanda do tráfego. |
| **3. Elasticidade** | Capacidade de **ajustar recursos automaticamente e em tempo real** para lidar com picos de demanda inesperados. Evita a queda do seu serviço sob estresse. | Serviços como **Azure Functions** ou **Azure Logic Apps** oferecem elasticidade, permitindo que você execute funções de maneira eficiente e econômica. |
| **4. Confiabilidade** | **Infraestrutura resiliente** que minimiza falhas. Seu provedor de nuvem cuida do hardware e da rede subjacentes. | A infraestrutura do Azure tem várias camadas de redundância, como **backup automatizado** e recuperação de desastres. |
| **5. Previsibilidade** | Confiança no desempenho e custo dos seus serviços. | O Azure oferece **Azure Cost Management** para ajudar a controlar e monitorar os custos de suas implementações. |
| **6. Segurança** | O provedor garante a **segurança da Nuvem** (o *data center*, o hardware). Você é responsável pela **segurança *na* Nuvem** (seu código, suas configurações de rede e IAM). | Usar **Azure Active Directory (AAD)** e **Azure Key Vault** para proteger segredos e gerenciar identidades. |
| **7. Governança** | Capacidade de **auditoria e sinalização** (monitoramento e *logging*). Crítico para rastrear problemas, segurança e conformidade. | Ferramentas como **Azure Monitor** e **Azure Security Center** fornecem monitoramento contínuo e alertas. |
| **8. Gerenciabilidade** | A Nuvem oferece **múltiplas ferramentas de gerenciamento** (portal da web, CLI, APIs). Você pode automatizar *deployments* usando a **CLI** ou **APIs**, um requisito em ambientes DevOps. | O **Azure CLI** e o **Azure DevOps** permitem automação e integração contínua no gerenciamento de serviços na nuvem. |

# 🤍 IV. Tipos de Serviço (Azure):

Os tipos de serviços na nuvem definem o nível de controle que você tem sobre sua aplicação e a infraestrutura necessária para gerenciá-la. A escolha do tipo de serviço tem um impacto direto no tempo de desenvolvimento, custo e manutenção da aplicação.

*Exemplo Prático:*
- Se você for criar uma aplicação simples de API, o **PaaS (Azure App Services)** pode ser a melhor escolha. Se você precisar de mais controle sobre o ambiente, como escolha do sistema operacional e das configurações de rede, o **IaaS (Azure Virtual Machines)** seria mais adequado.

| Tipo de Serviço | O que é | Foco/Vantagem | Responsabilidade do Cliente (Sua) |
| :--- | :--- | :--- | :--- |
| **IaaS** (Infraestrutura como Serviço) | O provedor aluga o **hardware** (servidores virtuais, armazenamento). | **Mais flexível**. Você configura o sistema operacional, runtime e aplicativos. Ideal para migrar aplicações antigas (*lift and shift*). | **S.O., Middleware, Aplicação, Dados.** |
| **PaaS** (Plataforma como Serviço) | O provedor gerencia o S.O., middleware e runtime. | **Foco total em desenvolvimento de aplicativos**. Você só se preocupa com o seu código. Ex: Azure App Services. | **Aplicação, Dados.** |
| **SaaS** (Software como Serviço) | O software é hospedado e gerenciado pelo provedor e usado via web. | **Pagamento conforme o uso** (modelo de assinatura). Ex: Microsoft 365, Gmail. | **Uso do software.** |


# 📘 V. O Modelo de Responsabilidade Compartilhada

**Conceito-Chave:** Você e o Provedor Estão Juntos!  
Neste modelo, a segurança na nuvem é uma responsabilidade compartilhada entre você e o provedor de nuvem. 

*Exemplo prático:*
- Quando você configura um banco de dados no **Azure SQL Database**, a segurança física do data center (onde o banco está armazenado) é responsabilidade do Azure. Porém, a configuração do banco, como controle de acesso e criptografia de dados, é de sua responsabilidade.

**Dicas Importantes:**
1. **Segurança na nuvem:** Nunca subestime a importância da segurança. As ferramentas de segurança fornecidas pela Azure, como **Azure Security Center** e **Azure Key Vault**, são essenciais para proteger dados e aplicações.
2. **Automatização de deploy:** Automatizar o processo de deploy usando ferramentas como **Azure DevOps** e **Terraform** pode reduzir erros humanos e melhorar a eficiência.

# 🖇️ VI. Conclusão e Próximos Passos

A computação em nuvem com Azure oferece um vasto leque de possibilidades para desenvolvedores Back-End. Compreender os conceitos fundamentais da nuvem, escolher os serviços certos e adotar práticas de segurança e governança são passos essenciais para uma carreira sólida na área.

**Links úteis** -> 
* [Documentação da Azure (pt-BR)](https://learn.microsoft.com/pt-br/azure/);
* [Microsoft Learn – Trilha de Aprendizado do Azure (pt-BR)](https://learn.microsoft.com/pt-br/training/azure/);
* [Certificações Microsoft Azure (pt-BR)](https://learn.microsoft.com/pt-br/certifications/);
* [Azure Blog Oficial](https://azure.microsoft.com/pt-br/blog/).



