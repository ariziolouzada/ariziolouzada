<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ariziolouzada.ati._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>ATIST</title>
    <meta content="" name="Assessoria de Tecnologia da Informação na Segurança do Trabalho" />
    <meta content="" name="Arizio Aguilar Oliveira LOuzada" />

    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700" rel="stylesheet" />
    <link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="assets/css/animate.min.css" rel="stylesheet" />
    <link href="assets/css/style.min.css" rel="stylesheet" />
    <link href="assets/css/style-responsive.min.css" rel="stylesheet" />
    <link href="assets/css/theme/default.css" id="theme" rel="stylesheet" />
    <!-- ================== END BASE CSS STYLE ================== -->

    <!-- ================== BEGIN BASE JS ================== -->
    <script src="assets/plugins/pace/pace.min.js"></script>
    <!-- ================== END BASE JS ================== -->

</head>
<body>
    <form id="form1" runat="server">
        <!-- begin #page-container -->
        <div id="page-container" class="fade">
            <!-- begin #header -->
            <div id="header" class="header navbar navbar-transparent navbar-fixed-top">
                <!-- begin container -->
                <div class="container">
                    <!-- begin navbar-header -->
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#header-navbar">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a href="default.aspx" class="navbar-brand">
                            <span class="brand-logo"></span>
                            <span class="brand-text">
                                <span class="text-theme">ATIST</span>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblSaldacao" runat="server" Text="Bom Dia."></asp:Label>
                            </span>
                        </a>
                    </div>
                    <!-- end navbar-header -->
                    <!-- begin navbar-collapse -->
                    <div class="collapse navbar-collapse" id="header-navbar">
                        <ul class="nav navbar-nav navbar-right">
                            <li class="active dropdown">
                                <a href="#home" data-click="scroll-to-target" data-toggle="dropdown">HOME <b class="caret"></b></a>
                                <%--<ul class="dropdown-menu dropdown-menu-left animated fadeInDown">
                                <li><a href="index.html">Page with Transparent Header</a></li>
                                <li><a href="index_inverse_header.html">Page with Inverse Header</a></li>
                                <li><a href="index_default_header.html">Page with White Header</a></li>
                                <li><a href="extra_element.html">Extra Element</a></li>
                            </ul>--%>
                        </li>
                            <li><a href="#about" data-click="scroll-to-target">SOBRE NÓS</a></li>
                            <li><a href="#team" data-click="scroll-to-target">EQUIPE</a></li>
                            <li><a href="#service" data-click="scroll-to-target">SISTEMAS</a></li>
                            <li><a href="#work" data-click="scroll-to-target">TREINAMENTOS</a></li>
                            <li><a href="#client" data-click="scroll-to-target">CLIENTES</a></li>
                            <%-- <li><a href="#pricing" data-click="scroll-to-target">PREÇOS</a></li>--%>
                            <li><a href="#contact" data-click="scroll-to-target">CONTATO</a></li>
                        </ul>
                    </div>
                    <!-- end navbar-collapse -->
                </div>
                <!-- end container -->
            </div>
            <!-- end #header -->

            <!-- begin #home -->
            <div id="home" class="content has-bg home">
                <!-- begin content-bg -->
                <div class="content-bg">
                    <img src="assets/img/home-bg.jpg" alt="Home" />
                </div>
                <!-- end content-bg -->
                <!-- begin container -->
                <div class="container home-content">
                    <h1>Assessoria de TI na Segurança do Trabalho</h1>
                    <h3>Bem vindo ao nosso site.</h3>
                    <h4>Produtos oferecidos para todos os tipos de empresas, independente do setor de atuação.                   
                        <br />
                        Nossos programas de assessorias envolvem diferentes segmentos da Indústria, Comércio e Serviços.<br />
                    </h4>
                    <%-- <a href="#" class="btn btn-theme">Explore More</a> <a href="#" class="btn btn-outline">Purchase Now</a><br />
                <br />
                or <a href="#">subscribe</a> newsletter--%>
                </div>
                <!-- end container -->
            </div>
            <!-- end #home -->

            <!-- begin #about -->
            <div id="about" class="content" data-scrollview="true">
                <!-- begin container -->
                <div class="container" data-animation="true" data-animation-type="fadeInDown">
                    <h2 class="content-title">SOBRE NÓS</h2>
                    <p class="content-desc">
                        Oferecer serviços, sistemas e treinamentos com qualidade, superando as expectativas de clientes e parceiros, investindo na capacitação profissional 
                   
                        <br />
                        e na melhoria continua de nossos métodos e processos, assegurando a excelência no segmento de atuação da empresa.          
               
                    </p>
                    <!-- begin row -->
                    <div class="row">
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-6">
                            <!-- begin about -->
                            <div class="about">
                                <h3>Nossa História</h3>
                                <p>
                                    A Assessoria de Tecnologia da Informação na Segurança do Trabalho foi criada com sede em Vitória-ES
                                idealizada pelos irmãos Roberto e Arizio com a MISSÃO de manter uma relação ética e transparente com clientes, 
                                    colaboradores e parceiros visando uma base sólida para sustentação e crescimento de todos.
                                Observar a excelência em prestação de serviços e desenvolvimento de soluções inteligentes, criativas e econômicas em Segurança do Trabalho, 
                                    Meio Ambiente e Treinamentos às empresas clientes e tendo como VALORES de profissionalismo, ética, seriedade, fidelidade,
                                     respeito ao cliente, para alcançarmos excelência.                           
                                </p>

                            </div>
                            <!-- end about -->
                        </div>
                        <!-- end col-4 -->
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-6">
                            <h3>Nossa Filosofia</h3>
                            <!-- begin about-author -->
                            <div class="about-author">
                                <div class="quote bg-silver">
                                    <i class="fa fa-quote-left"></i>
                                    <h3>O nosso trabalho,<br />
                                        <span>é tornar o seu mais simples.</span></h3>
                                    <i class="fa fa-quote-right"></i>
                                </div>
                                <div class="author">
                                    <div class="image">
                                        <img src="assets/img/user-1.jpg" alt="Sean Ngu" />
                                    </div>
                                    <div class="info">
                                        AAOL
                                   
                                        <small>Desenvolvedor</small>
                                    </div>
                                </div>
                            </div>
                            <!-- end about-author -->
                        </div>
                        <!-- end col-4 -->
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-12">
                            <h3>Nossa Experiencia</h3>
                            <!-- begin skills -->
                            <div class="skills">
                                <div class="skills-name">Front End</div>
                                <div class="progress progress-striped">
                                    <div class="progress-bar progress-bar-success" style="width: 95%">
                                        <span class="progress-number">95%</span>
                                    </div>
                                </div>
                                <div class="skills-name">Programming</div>
                                <div class="progress progress-striped">
                                    <div class="progress-bar progress-bar-success" style="width: 90%">
                                        <span class="progress-number">90%</span>
                                    </div>
                                </div>
                                <div class="skills-name">Database Design</div>
                                <div class="progress progress-striped">
                                    <div class="progress-bar progress-bar-success" style="width: 85%">
                                        <span class="progress-number">85%</span>
                                    </div>
                                </div>
                                <div class="skills-name">Wordpress</div>
                                <div class="progress progress-striped">
                                    <div class="progress-bar progress-bar-success" style="width: 80%">
                                        <span class="progress-number">80%</span>
                                    </div>
                                </div>
                            </div>
                            <!-- end skills -->
                        </div>
                        <!-- end col-4 -->
                    </div>
                    <!-- end row -->
                </div>
                <!-- end container -->
            </div>
            <!-- end #about -->

            <!-- begin #milestone -->
            <div id="milestone" class="content bg-black-darker has-bg" data-scrollview="true">
                <!-- begin content-bg -->
                <div class="content-bg">
                    <img src="assets/img/milestone-bg.jpg" alt="Milestone" />
                </div>
                <!-- end content-bg -->
                <!-- begin container -->
                <div class="container">
                    <!-- begin row -->
                    <div class="row">
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-3 milestone-col">
                            <div class="milestone">
                                <div class="number" data-animation="true" data-animation-type="number" data-final-number="1292">1,292</div>
                                <div class="title">Themes & Template</div>
                            </div>
                        </div>
                        <!-- end col-3 -->
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-3 milestone-col">
                            <div class="milestone">
                                <div class="number" data-animation="true" data-animation-type="number" data-final-number="9039">9,039</div>
                                <div class="title">Registered Members</div>
                            </div>
                        </div>
                        <!-- end col-3 -->
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-3 milestone-col">
                            <div class="milestone">
                                <div class="number" data-animation="true" data-animation-type="number" data-final-number="89291">89,291</div>
                                <div class="title">Items Sold</div>
                            </div>
                        </div>
                        <!-- end col-3 -->
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-3 milestone-col">
                            <div class="milestone">
                                <div class="number" data-animation="true" data-animation-type="number" data-final-number="129">129</div>
                                <div class="title">Theme Authors</div>
                            </div>
                        </div>
                        <!-- end col-3 -->
                    </div>
                    <!-- end row -->
                </div>
                <!-- end container -->
            </div>
            <!-- end #milestone -->

            <!-- begin #team -->
            <div id="team" class="content" data-scrollview="true">
                <!-- begin container -->
                <div class="container">
                    <h2 class="content-title">Nosso Time</h2>
                    <p class="content-desc">
                        O desenvolvimento das pessoas que trabalham em nossa organização é um dos pontos essenciais para o bom funcionamento da empresa. 
                        <br />
                        Quando as qualidades humanas se unem às qualidades profissionais, o resultado sempre é o mesmo: <b><i>CLIENTES SATISFEITOS</i></b>.
                    </p>
                    <!-- begin row -->
                    <div class="row">
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-4">
                            <!-- begin team -->
                            <div class="team">
                                <div class="image" data-animation="true" data-animation-type="flipInX">
                                    <img src="assets/img/user-1.jpg" alt="Roberto Oliveira" />
                                </div>
                                <div class="info">
                                    <h3 class="name">Roberto Oliveira</h3>
                                    <div class="title text-theme">FUNDADOR</div>
                                    <p>Formado em Seguranca do Trabalho pelo IFES em 20...</p>
                                    <div class="social">
                                        <a href="#"><i class="fa fa-facebook fa-lg fa-fw"></i></a>
                                        <a href="#"><i class="fa fa-twitter fa-lg fa-fw"></i></a>
                                        <a href="#"><i class="fa fa-google-plus fa-lg fa-fw"></i></a>
                                    </div>
                                </div>
                            </div>
                            <!-- end team -->
                        </div>
                        <!-- end col-4 -->
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-4">
                            <!-- begin team -->
                            <div class="team">
                                <div class="image" data-animation="true" data-animation-type="flipInX">
                                    <img src="assets/img/user-2.jpg" alt="Jonny Cash" />
                                </div>
                                <div class="info">
                                    <h3 class="name">Arizio A O Louzada</h3>
                                    <div class="title text-theme">PROGRAMADOR WEB</div>
                                    <p>Formado em Análise e Desenvolvimento de SIstemas pelo IFES em 2010.</p>
                                    <div class="social">
                                        <a href="#"><i class="fa fa-facebook fa-lg fa-fw"></i></a>
                                        <a href="#"><i class="fa fa-twitter fa-lg fa-fw"></i></a>
                                        <a href="#"><i class="fa fa-google-plus fa-lg fa-fw"></i></a>
                                    </div>
                                </div>
                            </div>
                            <!-- end team -->
                        </div>
                        <!-- end col-4 -->
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-4">
                            <!-- begin team -->
                            <div class="team">
                                <div class="image" data-animation="true" data-animation-type="flipInX">
                                    <img src="assets/img/user-3.jpg" alt="Mia Donovan" />
                                </div>
                                <div class="info">
                                    <h3 class="name">Mia Donovan</h3>
                                    <div class="title text-theme">WEB DESIGNER</div>
                                    <p>Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. </p>
                                    <div class="social">
                                        <a href="#"><i class="fa fa-facebook fa-lg fa-fw"></i></a>
                                        <a href="#"><i class="fa fa-twitter fa-lg fa-fw"></i></a>
                                        <a href="#"><i class="fa fa-google-plus fa-lg fa-fw"></i></a>
                                    </div>
                                </div>
                            </div>
                            <!-- end team -->
                        </div>
                        <!-- end col-4 -->
                    </div>
                    <!-- end row -->
                </div>
                <!-- end container -->
            </div>
            <!-- end #team -->

            <!-- begin #quote -->
            <div id="quote" class="content bg-black-darker has-bg" data-scrollview="true">
                <!-- begin content-bg -->
                <div class="content-bg">
                    <img src="assets/img/quote-bg.jpg" alt="Quote" />
                </div>
                <!-- end content-bg -->
                <!-- begin container -->
                <div class="container" data-animation="true" data-animation-type="fadeInLeft">
                    <!-- begin row -->
                    <div class="row">
                        <!-- begin col-12 -->
                        <div class="col-md-12 quote">
                            <i class="fa fa-quote-left"></i>Passion leads to design, design leads to performance,
                            <br />
                            performance leads to <span class="text-theme">success</span>!  
                       
                            <i class="fa fa-quote-right"></i>
                            <small>Sean Themes, Developer Teams in Malaysia</small>
                        </div>
                        <!-- end col-12 -->
                    </div>
                    <!-- end row -->
                </div>
                <!-- end container -->
            </div>
            <!-- end #quote -->

            <!-- beign #service -->
            <div id="service" class="content" data-scrollview="true">
                <!-- begin container -->
                <div class="container">
                    <h2 class="content-title">Nossos Sistemas</h2>
                    <p class="content-desc">
                        Temos a solução que sua empresa precisa, através dos nossos sistemas realizamos todos os serviços relacionados a área de Segurança, 
                       
                        <br />
                        Meio Ambiente e Treinamentos, garantindo com qualidade o atendimento a todas as exigências legais e procedimentos seguros. Conheça este serviço
               
                    </p>
                    <!-- begin row -->
                    <div class="row">
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-4">
                            <div class="service">
                                <div class="icon bg-theme" data-animation="true" data-animation-type="bounceIn"><i class="fa fa-cog"></i></div>
                                <div class="info">
                                    <h4 class="title">PPP</h4>
                                    <p class="desc">
                                        Cadastro de dados para o <span class="text-theme"><b>Perfil Profissiográfico Previdenciario</b></span> rincipalmente condições ambientais, 
                                        com base nas fontes de origem PPRA ou LTCAT, gerando os formulários devolvidos pela Previdência Social.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <!-- end col-4 -->
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-4">
                            <div class="service">
                                <div class="icon bg-theme" data-animation="true" data-animation-type="bounceIn"><i class="fa fa-headphones"></i></div>
                                <div class="info">
                                    <h4 class="title">Gestão de EPI </h4>
                                    <p class="desc">
                                        Sistema para gestão dos Equipamentos de Proteção Individual que são fornecidos aos colaboradores da empresa.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <!-- end col-4 -->
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-4">
                            <div class="service">
                                <div class="icon bg-theme" data-animation="true" data-animation-type="bounceIn"><i class="fa fa-file"></i></div>
                                <div class="info">
                                    <h4 class="title">Well Documented</h4>
                                    <p class="desc">Ut vel laoreet tortor. Donec venenatis ex velit, eget bibendum purus accumsan cursus. Curabitur pulvinar iaculis diam.</p>
                                </div>
                            </div>
                        </div>
                        <!-- end col-4 -->
                    </div>
                    <!-- end row -->
                    <!-- begin row -->
                    <div class="row">
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-4">
                            <div class="service">
                                <div class="icon bg-theme" data-animation="true" data-animation-type="bounceIn"><i class="fa fa-code"></i></div>
                                <div class="info">
                                    <h4 class="title">Re-usable Code</h4>
                                    <p class="desc">Aenean et elementum dui. Aenean massa enim, suscipit ut molestie quis, pretium sed orci. Ut faucibus egestas mattis.</p>
                                </div>
                            </div>
                        </div>
                        <!-- end col-4 -->
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-4">
                            <div class="service">
                                <div class="icon bg-theme" data-animation="true" data-animation-type="bounceIn"><i class="fa fa-shopping-cart"></i></div>
                                <div class="info">
                                    <h4 class="title">Online Shop</h4>
                                    <p class="desc">Quisque gravida metus in sollicitudin feugiat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.</p>
                                </div>
                            </div>
                        </div>
                        <!-- end col-4 -->
                        <!-- begin col-4 -->
                        <div class="col-md-4 col-sm-4">
                            <div class="service">
                                <div class="icon bg-theme" data-animation="true" data-animation-type="bounceIn"><i class="fa fa-heart"></i></div>
                                <div class="info">
                                    <h4 class="title">Free Support</h4>
                                    <p class="desc">Integer consectetur, massa id mattis tincidunt, sapien erat malesuada turpis, nec vehicula lacus felis nec libero. Fusce non lorem nisl.</p>
                                </div>
                            </div>
                        </div>
                        <!-- end col-4 -->
                    </div>
                    <!-- end row -->
                </div>
                <!-- end container -->
            </div>
            <!-- end #about -->

            <!-- beign #action-box -->
            <div id="action-box" class="content has-bg" data-scrollview="true">
                <!-- begin content-bg -->
                <div class="content-bg">
                    <img src="assets/img/action-bg.jpg" alt="Action" />
                </div>
                <!-- end content-bg -->
                <!-- begin container -->
                <div class="container" data-animation="true" data-animation-type="fadeInRight">
                    <!-- begin row -->
                    <div class="row action-box">
                        <!-- begin col-9 -->
                        <div class="col-md-9 col-sm-9">
                            <div class="icon-large text-theme">
                                <i class="fa fa-binoculars"></i>
                            </div>
                            <h3>CHECK OUT OUR ADMIN THEME!</h3>
                            <p>
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus faucibus magna eu lacinia eleifend.
                       
                            </p>
                        </div>
                        <!-- end col-9 -->
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-3">
                            <a href="#" class="btn btn-outline btn-block">Live Preview</a>
                        </div>
                        <!-- end col-3 -->
                    </div>
                    <!-- end row -->
                </div>
                <!-- end container -->
            </div>
            <!-- end #action-box -->

            <!-- begin #work -->
            <div id="work" class="content" data-scrollview="true">
                <!-- begin container -->
                <div class="container" data-animation="true" data-animation-type="fadeInDown">
                    <h2 class="content-title">Nossos Treinamentos</h2>
                    <p class="content-desc">
                        Transforme a cultura de segurança da sua empresa e reduza os desvios, incidentes e acidentes.
                        <br />
                        Evite prejuizos como afastamentos, parada da produção, processos judiciais, e indenizações . Além de instruir no cumprimento das normas regulatórias para evitar multas.
                    </p>
                    <!-- begin row -->
                    <div class="row row-space-10">
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-6">
                            <!-- begin work -->
                            <div class="work">
                                <div class="image">
                                    <a href="#">
                                        <img src="assets/img/work-1.jpg" alt="Work 1" /></a>
                                </div>
                                <div class="desc">
                                    <span class="desc-title">Aliquam molestie</span>
                                    <span class="desc-text">Lorem ipsum dolor sit amet</span>
                                </div>
                            </div>
                            <!-- end work -->
                        </div>
                        <!-- end col-3 -->
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-6">
                            <!-- begin work -->
                            <div class="work">
                                <div class="image">
                                    <a href="#">
                                        <img src="assets/img/work-3.jpg" alt="Work 3" /></a>
                                </div>
                                <div class="desc">
                                    <span class="desc-title">Quisque at pulvinar lacus</span>
                                    <span class="desc-text">Lorem ipsum dolor sit amet</span>
                                </div>
                            </div>
                            <!-- end work -->
                        </div>
                        <!-- end col-3 -->
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-6">
                            <!-- begin work -->
                            <div class="work">
                                <div class="image">
                                    <a href="#">
                                        <img src="assets/img/work-5.jpg" alt="Work 5" /></a>
                                </div>
                                <div class="desc">
                                    <span class="desc-title">Vestibulum et erat ornare</span>
                                    <span class="desc-text">Lorem ipsum dolor sit amet</span>
                                </div>
                            </div>
                            <!-- end work -->
                        </div>
                        <!-- end col-3 -->
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-6">
                            <!-- begin work -->
                            <div class="work">
                                <div class="image">
                                    <a href="#">
                                        <img src="assets/img/work-7.jpg" alt="Work 7" /></a>
                                </div>
                                <div class="desc">
                                    <span class="desc-title">Sed vitae mollis magna</span>
                                    <span class="desc-text">Lorem ipsum dolor sit amet</span>
                                </div>
                            </div>
                            <!-- end work -->
                        </div>
                        <!-- end col-3 -->
                    </div>
                    <!-- end row -->
                    <!-- begin row -->
                    <div class="row row-space-10">
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-6">
                            <!-- begin work -->
                            <div class="work">
                                <div class="image">
                                    <a href="#">
                                        <img src="assets/img/work-2.jpg" alt="Work 2" /></a>
                                </div>
                                <div class="desc">
                                    <span class="desc-title">Suspendisse at mattis odio</span>
                                    <span class="desc-text">Lorem ipsum dolor sit amet</span>
                                </div>
                            </div>
                            <!-- end work -->
                        </div>
                        <!-- end col-3 -->
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-6">
                            <!-- begin work -->
                            <div class="work">
                                <div class="image">
                                    <a href="#">
                                        <img src="assets/img/work-4.jpg" alt="Work 4" /></a>
                                </div>
                                <div class="desc">
                                    <span class="desc-title">Aliquam vitae commodo diam</span>
                                    <span class="desc-text">Lorem ipsum dolor sit amet</span>
                                </div>
                            </div>
                            <!-- end work -->
                        </div>
                        <!-- end col-3 -->
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-6">
                            <!-- begin work -->
                            <div class="work">
                                <div class="image">
                                    <a href="#">
                                        <img src="assets/img/work-6.jpg" alt="Work 6" /></a>
                                </div>
                                <div class="desc">
                                    <span class="desc-title">Phasellus eu vehicula lorem</span>
                                    <span class="desc-text">Lorem ipsum dolor sit amet</span>
                                </div>
                            </div>
                            <!-- end work -->
                        </div>
                        <!-- end col-3 -->
                        <!-- begin col-3 -->
                        <div class="col-md-3 col-sm-6">
                            <!-- begin work -->
                            <div class="work">
                                <div class="image">
                                    <a href="#">
                                        <img src="assets/img/work-8.jpg" alt="Work 8" /></a>
                                </div>
                                <div class="desc">
                                    <span class="desc-title">Morbi bibendum pellentesque</span>
                                    <span class="desc-text">Lorem ipsum dolor sit amet</span>
                                </div>
                            </div>
                            <!-- end work -->
                        </div>
                        <!-- end col-3 -->
                    </div>
                    <!-- end row -->
                </div>
                <!-- end container -->
            </div>
            <!-- end #work -->

            <!-- begin #client -->
            <div id="client" class="content has-bg bg-green" data-scrollview="true">
                <!-- begin content-bg -->
                <div class="content-bg">
                    <img src="assets/img/client-bg.jpg" alt="Client" />
                </div>
                <!-- end content-bg -->
                <!-- begin container -->
                <div class="container" data-animation="true" data-animation-type="fadeInUp">
                    <h2 class="content-title">Our Client Testimonials</h2>
                    <!-- begin carousel -->
                    <div class="carousel testimonials slide" data-ride="carousel" id="testimonials">
                        <!-- begin carousel-inner -->
                        <div class="carousel-inner text-center">
                            <!-- begin item -->
                            <div class="item active">
                                <blockquote>
                                    <i class="fa fa-quote-left"></i>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce viverra, nulla ut interdum fringilla,<br />
                                    urna massa cursus lectus, eget rutrum lectus neque non ex.
                               
                                    <i class="fa fa-quote-right"></i>
                                </blockquote>
                                <div class="name">— <span class="text-theme">Mark Doe</span>, Designer</div>
                            </div>
                            <!-- end item -->
                            <!-- begin item -->
                            <div class="item">
                                <blockquote>
                                    <i class="fa fa-quote-left"></i>
                                    Donec cursus ligula at ante vulputate laoreet. Nulla egestas sit amet lorem non bibendum.<br />
                                    Nulla eget risus velit. Pellentesque tincidunt velit vitae tincidunt finibus.
                               
                                    <i class="fa fa-quote-right"></i>
                                </blockquote>
                                <div class="name">— <span class="text-theme">Joe Smith</span>, Developer</div>
                            </div>
                            <!-- end item -->
                            <!-- begin item -->
                            <div class="item">
                                <blockquote>
                                    <i class="fa fa-quote-left"></i>
                                    Sed tincidunt quis est sed ultrices. Sed feugiat auctor ipsum, sit amet accumsan elit vestibulum<br />
                                    fringilla. In sollicitudin ac ligula eget vestibulum.
                               
                                    <i class="fa fa-quote-right"></i>
                                </blockquote>
                                <div class="name">— <span class="text-theme">Linda Adams</span>, Programmer</div>
                            </div>
                            <!-- end item -->
                        </div>
                        <!-- end carousel-inner -->
                        <!-- begin carousel-indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#testimonials" data-slide-to="0" class="active"></li>
                            <li data-target="#testimonials" data-slide-to="1" class=""></li>
                            <li data-target="#testimonials" data-slide-to="2" class=""></li>
                        </ol>
                        <!-- end carousel-indicators -->
                    </div>
                    <!-- end carousel -->
                </div>
                <!-- end containter -->
            </div>
            <!-- end #client -->

            <!-- begin #pricing -->

            <%--            <div id="pricing" class="content" data-scrollview="true">
                <!-- begin container -->
                <div class="container">
                    <h2 class="content-title">Our Price</h2>
                    <p class="content-desc">
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum consectetur eros dolor,<br />
                        sed bibendum turpis luctus eget
               
                    </p>
                    <!-- begin pricing-table -->
                    <ul class="pricing-table col-4">
                        <li data-animation="true" data-animation-type="fadeInUp">
                            <div class="pricing-container">
                                <h3>Starter</h3>
                                <div class="price">
                                    <div class="price-figure">
                                        <span class="price-number">FREE</span>
                                    </div>
                                </div>
                                <ul class="features">
                                    <li>1GB Storage</li>
                                    <li>2 Clients</li>
                                    <li>5 Active Projects</li>
                                    <li>5 Colors</li>
                                    <li>Free Goodies</li>
                                    <li>24/7 Email support</li>
                                </ul>
                                <div class="footer">
                                    <a href="#" class="btn btn-inverse btn-block">Buy Now</a>
                                </div>
                            </div>
                        </li>
                        <li data-animation="true" data-animation-type="fadeInUp">
                            <div class="pricing-container">
                                <h3>Basic</h3>
                                <div class="price">
                                    <div class="price-figure">
                                        <span class="price-number">$9.99</span>
                                        <span class="price-tenure">per month</span>
                                    </div>
                                </div>
                                <ul class="features">
                                    <li>2GB Storage</li>
                                    <li>5 Clients</li>
                                    <li>10 Active Projects</li>
                                    <li>10 Colors</li>
                                    <li>Free Goodies</li>
                                    <li>24/7 Email support</li>
                                </ul>
                                <div class="footer">
                                    <a href="#" class="btn btn-inverse btn-block">Buy Now</a>
                                </div>
                            </div>
                        </li>
                        <li class="highlight" data-animation="true" data-animation-type="fadeInUp">
                            <div class="pricing-container">
                                <h3>Premium</h3>
                                <div class="price">
                                    <div class="price-figure">
                                        <span class="price-number">$19.99</span>
                                        <span class="price-tenure">per month</span>
                                    </div>
                                </div>
                                <ul class="features">
                                    <li>5GB Storage</li>
                                    <li>10 Clients</li>
                                    <li>20 Active Projects</li>
                                    <li>20 Colors</li>
                                    <li>Free Goodies</li>
                                    <li>24/7 Email support</li>
                                </ul>
                                <div class="footer">
                                    <a href="#" class="btn btn-theme btn-block">Buy Now</a>
                                </div>
                            </div>
                        </li>
                        <li data-animation="true" data-animation-type="fadeInUp">
                            <div class="pricing-container">
                                <h3>Lifetime</h3>
                                <div class="price">
                                    <div class="price-figure">
                                        <span class="price-number">$999</span>
                                    </div>
                                </div>
                                <ul class="features">
                                    <li>Unlimited Storage</li>
                                    <li>Unlimited Clients</li>
                                    <li>Unlimited Projects</li>
                                    <li>Unlimited Colors</li>
                                    <li>Free Goodies</li>
                                    <li>24/7 Email support</li>
                                </ul>
                                <div class="footer">
                                    <a href="#" class="btn btn-inverse btn-block">Buy Now</a>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <!-- end container -->
            </div>--%>

            <!-- end #pricing -->

            <!-- begin #contact -->
            <div id="contact" class="content bg-silver-lighter" data-scrollview="true">
                <!-- begin container -->
                <div class="container">
                    <h2 class="content-title">Entre em Contato Conosco</h2>
                    <p class="content-desc">
                        Entre em contato conosco e de sua sugestão ou crítica.
                        <br />Ela é muito importante para nós.               
                    </p>
                    <!-- begin row -->
                    <div class="row">
                        <!-- begin col-6 -->
                        <div class="col-md-6" data-animation="true" data-animation-type="fadeInLeft">
                            <h3>Se você tem um projeto que você gostaria de discutir, entre em contato com a gente.
                                </h3>
                            <p>
                               Você pode utilizar qualquer um dos contatos aqui relacionados.
                            </p>
                            <p>
                                <strong>Assessoria de Tecnologia da Informação na Segurança do Trabalho</strong><br />
                                Rua das Palmeiras, 220, Itararé<br />
                                Vitória-ES, CEP 29.047-550<br />
                                Fixo: +55 (27) 3315-3048<br />
                            </p>
                            <p>
                                <span class="phone">+55 (27) 99933-0097</span><br />
                                <span class="phone">+55 (27) 99825-9169</span><br />
                                <a href="mailto:contato@ariziolouzada.com.br">contato@ariziolouzada.com.br</a>
                            </p>
                        </div>
                        <!-- end col-6 -->
                        <!-- begin col-6 -->
                        <div class="col-md-6 form-col" data-animation="true" data-animation-type="fadeInRight">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-md-3">Nome <span class="text-theme">*</span></label>
                                    <div class="col-md-9">
                                        <input type="text" class="form-control" id="txtNome" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3">Email <span class="text-theme">*</span></label>
                                    <div class="col-md-9">
                                        <input type="text" class="form-control" id="txtEmail" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3">Assunto <span class="text-theme">*</span></label>
                                    <div class="col-md-9">
                                        <input type="text" class="form-control" id="txtAssunto" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3">Mensagem <span class="text-theme">*</span></label>
                                    <div class="col-md-9">
                                        <textarea class="form-control" rows="10" id="txtMensagem" runat="server"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3"></label>
                                    <div class="col-md-9 text-left">
                                        <asp:Literal ID="ltlMsn" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-md-3"></label>
                                    <div class="col-md-9 text-left">
                                        <asp:Button ID="btnEnviarMensagem" runat="server" Text="Enviar Mensagem" class="btn btn-theme btn-block" OnClick="btnEnviarMensagem_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col-6 -->
                    </div>
                    <!-- end row -->
                </div>
                <!-- end container -->
            </div>
            <!-- end #contact -->

            <!-- begin #footer -->
            <div id="footer" class="footer">
                <div class="container">
                    <div class="footer-brand">
                        <div class="footer-brand-logo"></div>
                        O nosso trabalho é tornar o seu mais simples.
                       
               
                    </div>
                    <p>
                        &copy; Copyright 2016
                        <br />
                        Assessoria de Tecnologia da Informação na Segurança do Trabalho
                        <br />
                        <a href="#">ATIST</a>
                    </p>
                    <p class="social-list">
                        <a href="#"><i class="fa fa-facebook fa-fw"></i></a>
                        <a href="#"><i class="fa fa-instagram fa-fw"></i></a>
                        <a href="#"><i class="fa fa-twitter fa-fw"></i></a>
                        <a href="#"><i class="fa fa-google-plus fa-fw"></i></a>
                        <a href="#"><i class="fa fa-dribbble fa-fw"></i></a>
                    </p>
                </div>
            </div>
            <!-- end #footer -->

            <!-- begin theme-panel -->
            <div class="theme-panel">
                <a href="javascript:;" data-click="theme-panel-expand" class="theme-collapse-btn"><i class="fa fa-cog"></i></a>
                <div class="theme-panel-content">
                    <ul class="theme-list clearfix">
                        <li><a href="javascript:;" class="bg-purple" data-theme="purple" data-click="theme-selector" data-toggle="tooltip" data-trigger="hover" data-container="body" data-title="Purple">&nbsp;</a></li>
                        <li><a href="javascript:;" class="bg-blue" data-theme="blue" data-click="theme-selector" data-toggle="tooltip" data-trigger="hover" data-container="body" data-title="Blue">&nbsp;</a></li>
                        <li class="active"><a href="javascript:;" class="bg-green" data-theme="default" data-click="theme-selector" data-toggle="tooltip" data-trigger="hover" data-container="body" data-title="Default">&nbsp;</a></li>
                        <li><a href="javascript:;" class="bg-orange" data-theme="orange" data-click="theme-selector" data-toggle="tooltip" data-trigger="hover" data-container="body" data-title="Orange">&nbsp;</a></li>
                        <li><a href="javascript:;" class="bg-red" data-theme="red" data-click="theme-selector" data-toggle="tooltip" data-trigger="hover" data-container="body" data-title="Red">&nbsp;</a></li>
                    </ul>
                </div>
            </div>
            <!-- end theme-panel -->
        </div>
        <!-- end #page-container -->
    </form>
    <!-- ================== BEGIN BASE JS ================== -->
    <script src="assets/plugins/jquery/jquery-1.9.1.min.js"></script>
    <script src="assets/plugins/jquery/jquery-migrate-1.1.0.min.js"></script>
    <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <!--[if lt IE 9]>
		<script src="assets/crossbrowserjs/html5shiv.js"></script>
		<script src="assets/crossbrowserjs/respond.min.js"></script>
		<script src="assets/crossbrowserjs/excanvas.min.js"></script>
	<![endif]-->
    <script src="assets/plugins/jquery-cookie/jquery.cookie.js"></script>
    <script src="assets/plugins/scrollMonitor/scrollMonitor.js"></script>
    <script src="assets/js/apps.min.js"></script>
    <!-- ================== END BASE JS ================== -->

    <script>
        $(document).ready(function () {
            App.init();
        });
	</script>

</body>
</html>
