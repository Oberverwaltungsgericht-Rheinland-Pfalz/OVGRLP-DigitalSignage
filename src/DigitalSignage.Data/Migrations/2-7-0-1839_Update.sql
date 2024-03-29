﻿CREATE TABLE [dbo].[Permissions] (
    [Id] [int] NOT NULL IDENTITY,
    [Ressource] [nvarchar](max) NOT NULL,
    [Member] [nvarchar](max) NOT NULL,
    [GET] [bit] NOT NULL,
    [PUT] [bit] NOT NULL,
    [POST] [bit] NOT NULL,
    [DELETE] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Permissions] PRIMARY KEY ([Id])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201809270719557_2-7-0-1839_PermissionsAdded', N'DigitalSignage.Data.Migrations.Configuration',  0x1F8B0800000000000400ED5DDB6EE3BA157D2FD07F30FCD41673E249A6A76807CE39C87510CC4C12C4C9F4A02F03C6661C6174712579904CD12FEB433FA9BF50CA12A5CDAB489992E5C4C84BCCCBDAE4E6E27D8BFB7FFFF9EFF8D7A7C01F7CC771E245E1E1707FEFED7080C36934F3C2F9E170993EFCF4D7E1AFBFFCFE77E3B359F034F842D3BDCBD2919C6172387C4CD3C5FBD128993EE200257B81378DA3247A48F7A6513042B36874F0F6EDDF46FBFB234C2086046B3018DF2CC3D40BF0EA07F979128553BC4897C8FF1CCDB09F14E12466B2421D5CA200270B34C587C3536FEEA5C89F78F310CDF1DE294AD17070E47B88146582FD87E100856194A29414F4FD5D8227691C85F3C9820420FFF6798149BA07E427B8A8C0FB2AB9695DDE1E647519551929D47499A4516009B8FFAE50CE88CFDE48C5C35279447D6744CDE97356EB950A0F87C728F1A6C970C08B7A7FE2C7593241BF17E1438C92345E4ED3658CF7F2F6D93B23FF7F43E768FAB89723BE1968F3BD29A9F3766FF5F76670B2F4B398C3102FD318F96F06D7CB7BDF9B7EC4CFB7D1371C1E864BDF877521B521714C0009BA8EA3058ED3E71BFC50D4F0721904381E0E466CDE119FB9CCCAE5CBB57011A6EF0E86834B520874EFE39232406393348AF1071CE218A578768DD214C7A4C52F6678A574A1049CBC0F38F6A68F6912925F542AE12AE977C3C167F4F40987F3F4F17048FE1D0ECEBD273CA3214549EE428F74539289E818D709FBB8C4F10FECB72E278D7EFBFCE99C288DA8E3B175694F899F76266CEE47F7C86FAF7AE351D559F55D182738FDB1CC443AECC514B43F1DB92C527231B3EFCE6CEEAE3A3551CE037A8C71C84AFDCB9F2552F54837D9E0508D4712921DFCFCB311C9B4828D5977EA250B1F3DAFCDB9094E53529364AF00EC0FDF9AB0AC4B6E5D763151DC7AA9DF81141C90B64FDB1734499F7D9CB42EE6DCF3F57DD58D980F71B45CB42E852CC6C992D9BF8BDB5F2E5CE2F468368B71D27E13FD3DF22F161D0AFB8CA61D4ABB9B2DAEA338AD1B8CF438A73899C6DE22DF79B45CE653B2DA7EA6528EA3C8C728AC2FF025FAEECD5783294FA428C5C9519290392420A329D1F90DF6570993476F916F0CF7B244559AAFE58C761E47C14DE41730628AAFB7289EE34CB99136D9245AC6D3A6F32B8BE96E9A657177B3ADF16C4BF4562FAF6EF641559F3C25C5B8F582DA4C67E1CC36CB4914E4A469BBD3E644B7568BB2DB965DD0656FA5DDB0A6B7D24E6D33C4280BFA551C80D8720A09A4838A986AED31C5ED48B21B3F3A5EAD5B8E38D9DAAD8B91E03C22AC9C6D60FE6ED0D164E381BA3B36EA68D7649CC71E0E8FBEA5DE7797C7320C707F3A5F5EAC265DB0CAB97D473279D91D9CC8ACC5B163ECCD49DF20B56F836815FA8E6D3BB6E1F01A9131B295212D47DEB16CC7321C4EBEB4C1B0C9971DBB76ECC2E13FF072DECE6C9923EF58F6AA5986E3C04B92D5A1A9ABAD7685D91F6EF57DC37D839384EEF43ADE757FC6C1BD93EB1F4BB91FCE6EADB7E07CDFB95B1FE26AB236C6E9D9A7B3DB333B14F31E1A473F8EF177F79B7380DB9F7E5A94AAD1245065DDC259E078633340AEB69636E63CF88E69AF9E692D6CCA21F08E61AF9761931405C18C54DFE92056A1F6875B55999AD08BCDDD339B6127968192451A4A9741076295B7446507935E0F554DF21524AC6E8764F1C2E59034D15A7743A02CEE3A5409DA9FFEC48E7EB6FDC966EC74D79F6C7AB11EE9D3C32C2C7780C7CFA97DEFFA88E0F7078D20265E6EF19C20A4B3DD6B697880D22F63AE11F559EF1EE31FD84BAF7DA4B33C6BA9DC8570B28D5B627F137A235C5EEAAC049B4CF8A2982BFCF0407A05E9EABA2F175AAAE3D18FEE657EC073329CA4A8B2A7EA4EF631267DF91BE90DFBADB76B29EAA0755147B1CEB2A4F5E91F7CF82299FEABE91AA4AB667F49B430F9CBD2D89A8971A620DA82726965856592680ACCA66B5A687884615472984157FC2A9D411D40E2A615A13B64A34AD0C4BA0AE4690C0A5F246C5AF0EC26D4A8D059425D81275F0C0A4B12352D28BD54332A2C4DAC2B709EC6A0D04542EB8233A7C0FA623349A5850629744586C91A16D8BC4F8AE9354537EB9142DA869530EB8F6C5A4DE1EB7B2393CEB6D0F010C4D5469337F7D5EE464D369A4749124DBD55D9A49F109416D36CA5CFC2D9C0CC7C3A9FE745536C32E9932DA4B7202B4952ACC3E19F04C5D6CA289551C928EDBB59F0FD21BFF3BC0A4FB18F533C389AE69FD89FA0644AB829EE394931D810B259C571B63344FE096951B2FDF5C254DCD97AE1D45B20DFA80A5C6EC39BD3AC74A51C3EE6142F70986D608DDAC9A400C06E5F2C47298ED35E9DB2C623C0C07A628A86BB3ACE68AC78595AB6CA97BA3249686CDB551A5352A9A18E18A9D486897CFA71CD46D828DB28A8DA5DBB6BA85A1D4C039DD051B75101C5021BA15678A8D18E090B742786567CD4A8C345393AE524BB27AC27806283D8076ECAF7A4A068DCFEB7658E4A35B5219E4A55B3B55C055B0E7356C836207D62AD64D323A12EDC6D75C45F51711B26B1A8A9AD6572B1F7346709BF13ED1383B9DDAF84BD74ABDD117359656D98B5AC76B696B1932F166C8587827D622A388794B0343BEAEC88A1958236CCCE4A235BCBCCE2D0D79C05FC09709F18CA1D3A4B584ACFB83B622AABAC0DB395D5CEF631169EF71B70427AF8DF0BB6CAEE1B2057998B8DB6992A51D3A6782AD1CBB6B2D46EB3A5BEECE9115F6BB65AE2BD5637CCEDCF464BA5A66DE5B0F9364B7EDFD723EE6A3659EC7566379CEDC7164BA69A2DE1AAF47257C506BD4971450578A1DC094DF557D006DDC7113F750A322184CE48DF8A9C3A85B828887366E697FED97B4E24078ECB9B5C68EC7D7ABF7AEFE949F65CE05D820BEBF2A430BCE3B992E14F704A8DEB8A87E12B63037A8954440877FC5CFEEAAE4984A8E26A508A9B6A59394A03811A08F6065686C4DFD11A00AA606A3373571C0208176F8806D7424A4898C81097CE554A4C9AC0102F3BB9516265918638746FADC4A209EAF0CAEFEA650D0ABFE4AF036236522212136D86A56F53318D19AABA45D9F81A343881095030B20607CC37020C88E350C010A9EAE7D5FB9020B1FE25497E0C37B68C2A2B25196C8499C1D8160AA056A3203FE5B39A30D492E4CD3CB9926A6C7484DAA8AD743815D52B466D5C63AEED06EA911A8D8BCAA9351951EC1144A311501B25DF6BC124AA514FB1EB28859B9D748AD1D82DD41CADB2E7686B28486E6F0000F593AD0B45C101DA445BAAC39C9A1A4ACE731CE84D720C23519E660E72A1413A2199684F768C505347EE24C181D6B8030089C61473AC0B6D656B28134DF1B784E6F7840E34046EF724DA912C035D6886AE084DB423BBA9B2BBAB72A025EE8649A229C522772D6D310B55ADAE94772436B724EBE84976B701B5A45B733BD091F1D0AE3FA6B73EA85F5F6535E37AEDD6C281F28C4675F5D9B0D5E9F0FA0AD30CE9DA5D93BDA2E49F92885AAA3F95343F9704D551EFDCCC4F134DD4ADD10CFDBCA53CE92AE3C6A3DCF96311301E29BC448E3FA3C5227B36AFCA59840C26B9CBC8939F26F6AE14831C6334657641FCB95C29298D6234C75C6CF620FB0C9F7B7192667E2AEF51F6B9EBC92C1092A9CEF514BB692A9539BA13DB8F6EB069F2EC7FD939E2CA89E69E1CA6D2E639A960B6EF5AD515C3BD8722E32073DF897C14739F57514F8B2791BF0C42368C67A11A857D0B0562B131E688A593440856069AE3704E10211A17658EC9BA3A84906C8C39A2C49F218495448BD8E311C70DE15C5C20A17087C152DA8CF09A4DB825E7554826B457E755E99C754A08D5AD7376A843646FCC20A2FE2E4D8D583A1E846065606F38509E91AD4B01C571A00101943955AAE59BC8AE612E8581EED272802B9CFB418822C802A374DDC7C094A1E648D4371FC4A161E628D4F51E44A161169349EE598F9945F220730CE8370F02C1708BD606AEF1983607E1E668ACF73B88C7C658214217771C248CB2C22C1DD9718065B8391AE3CE0EC231111678B9CB3A06290FEACDA0C81F90AF3B367237A5F643641D404B2365F17929D36F149F9CAA510AEF6DECF084EC38B872E60611560136634AE1D98D1D5002D9C5B50E077C01CEF057FD61F84639EC86B90DF9BA3DF379E9ED8B9F6E2CD941BD7931736811D61B5ED45D30D9F1438B6640949AFC2A4D576E16A0AE556E1B7448EE17FED48D8258B21EB2407BBCDA8C0A6A480B3EE84076A4689914CAA3E3668490C359904105B02342CB44905E773623810865410059E65DE3B7DCF8CADBDC660490C359904005B023425B4400569E6BB3A0B2096DC0004DE676F614C00912737C5B059B6351AF46108886599CAC653E8A9873B52CC08271775CFE558045FE95772206601562B1772E7C13311BE722AC3F9CD79B6758925E0366427B6D766543550E3B98D6026017A71B1EFD8EB9B21DF78F012E37467588E65C68B42DDA11C205215C6D8A7468E644B0DE12ED48B016098085CFBA0C50431934BF2EB3E6001E7C73C79DC36BBEC65323BAB719297CE2306B833CA83714D0198CD93140896440004DDEEEBAAA7B46157E5F20541164617784447B281A665137C6F90B533726A619E265ACC6BCB42A27E3EB05423211D678A5FB1609641967C5949553168E23AB307314C6E70A846222CCF132372A1026FB6D33FA550E51D8B1AF0A3747832E4E584BA62ABC01DA8102EDC04A4FFCC5ED91FCDAB6D51159B06CE59394D28B90F27769D95A589532E6AEAB3A67C6ABABBA2685852B6F669A27190E8862BE7BB3CCC474F29CA43828A6F27FFA27BEB7BA25A4093EA3D07BC0499A3B011B1EBCDD271A3FF23D94E426C88501ED7BFE4373238BDAFD7799452D9E05233EBBBD5D6E86922433C64799E8758D7EB52E7C177F11CEF0D3E1F05F837F377172263753AD776F46F3E59FBB7A99E26B5D9759BB3B127D0386DF513C7D44F11F02F4F4478868E2EDA7B47C7580C5D9BD3A4064CD5E1D004A0C5EAD518D9D0282CF351D335467485ACF5336778B6C655770B9A07BB2E40C656F59941AC9F2BE1F5CFCF615667F33B88AC900F67EF09668CCB61CA5612BDBD6A2632B37AEC8E53E398C1AB64973B6DC8897AE869AC20AD505526988EA008C5AA33A80A226A90EA00AC3540748D032D5011C344D7500C75AA6BA018476A96E104BC354D0CF6CFDEA427B5407A52AAC52E9A05A5720E3C14B67CCB9BD635861170A4418CC3F79AEB5669EC29634979B9D3CA45E806D9BFAAC726ED914A2B42675C1BCCA9ED44E9F65460B955AF1F6A5B0D564C635422ACD445D4C6E85ADA8F311877BB0C3F15259655251DFCA55CE57B044A6F61DE62BE4353646B2574676CDFE6A9A9D7E58BF6BF257D3E4D9FB2FBBE67E35CD4D1FB1D935F9CB6F7285F9E3D6AEBE8119A58B253835A67481B5B2AA345D7F4B7871B75EF6954965F3FCD49CD2FD06827914CAF598A33542321879B8FCAF61FC39EE6AEC115FB5DAB5FE6B6BFDB67613BB96EF6DCBC317CD1C37BBCE44ACBED1D9DC1BB203B0BCD6944CD4B96D633797A5E03939C74DA9B31FAC6F4A5DA772DD9A4ADA18F45E9877ADDE5B5834E6D2532F7C6E72CB452D1AD743616D191DB39B376A649BD6EE4C9CB166745C4EDEACD1B51A0A3BC775066C11953179745CE2CC0ED23124348A740C0D2D24DDEA185A4BBA453E8A85AB2247C3BEE882C9C45F03789DB4F4FC90178F7516D389B725DEA10C28081FD58A9725F5137626C72A031B3F4AB52F428912358F0C75E2D1CBC0B305D796420BBE121E291E4DEA038954CF7775ECBF50F1E6A7EC31E98DBA2904F6A5A01420B45D6F84DD3A1ED43DE26A2F7623BEB20D5D0F6F94539C3106280917F392B855F74ED896F0CBC669702F48060F6E254CEBCE19F066E8A6FDE67E4B3867EAE4B7177CA347C512AE75E3C077333C537ECEBF251CE31FD8EA2DBF322B1709B7B2E097C82BE9AB695BC229D97B5DBDE51535A791708B46BD447E291F65EB3BC7944F23F58F618CE904E41713F1A2D8A57FF76A3BB8B53D4B7DD1444364D98B5DE81BBCADB51D7CDB8E653E6B1022F2EC452EF26B9EECEA25BFEA3DDD491DD3316D0A833BA19725C91DD14AFB20979DFD8A15AFECE85C27D639A7B85757CAEFDD78477C7C63160FB9D0ABD4E20191EAD2921EA21711F90B2B87C3D97D445A3DBFF6A471A31AECEA1C5E84AFE2A412947EBE7921E515A820A28C910950FA9EE7F1F91B29410C9F4026ADCEA3BB4CA842945A403D2C77802DE073F132417ADFE22A817079A3940A13E9446B5CB8AAE4D3694F299B26D0C955784355C9CCCE3C94F2B2489D2C89B768951CBAFF55CAA20974F2143E970599D5A70E127920522A0BBC575D2B88D97C89A29868A9309D9B6485343D4BC5341AB9361C6557662AC15A866ADDF5F202E1B24190062365A2D4AE740539605D2088017132296A0FBB52F7C20A4B1A898361139B1B66C9C34D2F8C4344719657DB3880EC7C14BF00B3F6AE2CB7FC50D4BEC64444A88450F4DE545B66AEA0F5BCADF3A72A59E282B29B39D816D63B9CFF549755E6E661ADC3F11A8F522EAA2E5D59483C48B5A102638FF5263E955C2A439C4F543E94DA508B912FFA3A8F422ED5C1CE70320F426DA8215B4999A840EA4BC765F5ABF520EF3BA78D6AD3059D49D5959E645C569F5DA6CA3CC7385503B3FCD32A41EF51C2890A248B59D183440BD5371E190D7C2A3854846A5CAC5D3A3B5089D1A858E351C0A12A6463A276316FAF02E949A3A4FEF52792CA3349507AF5FE601DD569AA2D3C3F5CC68D47F9D6A208203F856786C7A39B6598BD2696FF3AC5D9F2B4841813CC104F9983B732CD45F810D15340AE443489F01D7C8A8866D0519C7A0F689A92E829268D9CAD0CBF207F49929C05F77876115E2DD3C5322555C6C1BDCFEC33B273449DFCF14828F3F86AF5E45EE2A20AA4985ED6B857E1F1D2F36765B9CF25DF412820B203CAE2C3B1AC2DD3EC03B2F97389741985864085FACA7355FA246672154ED077DCA46C7709FE84E768FA7C5DBC16AD06A96F0856EDE3530FCD6314240546959FFC241C9E054FBFFC1FBF65139EA8F80000 , N'6.2.0-61023')

