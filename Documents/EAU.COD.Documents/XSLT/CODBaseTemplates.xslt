<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
                xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
                   xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                  xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                 xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
                  xmlns:pad="http://ereg.egov.bg/segment/R-3165"
                 xmlns:ca="http://ereg.egov.bg/segment/R-3164"
                 xmlns:ead="http://ereg.egov.bg/segment/R-3166"
                 xmlns:popssl="http://ereg.egov.bg/value/R-2121"
                xmlns:so="http://ereg.egov.bg/segment/R-3176"
                   xmlns:ppp="http://ereg.egov.bg/segment/R-3169"
                  xmlns:ps="http://ereg.egov.bg/segment/R-3167"
                 xmlns:aasa="http://ereg.egov.bg/segment/R-3170"
                 xmlns:st="http://ereg.egov.bg/segment/R-3168"
                xmlns:soe="http://ereg.egov.bg/segment/R-3173"
                   xmlns:stc="http://ereg.egov.bg/segment/R-3174"
                xmlns:poap="http://ereg.egov.bg/segment/R-3175"
                 xmlns:sppp="http://ereg.egov.bg/segment/R-3171"
                 xmlns:sosre="http://ereg.egov.bg/segment/R-3172"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xslExtension="urn:XSLExtension">

  <!-- Custom Variables -->

  <!-- Head Css Styles -->

  <xsl:template name="Head">

    <head>
      <style>
        .digital-stamp {
        display: inline-block;
        vertical-align: middle;
        padding: 0.3125rem;
        margin-left: 1.875rem;
        font-family: Roboto, Arial, "Segoe UI", "Helvetica Neue", Verdana, sans-serif;
        border: 0.1875rem solid #a0cade;
        border-radius: 0.625rem;
        }

        .digital-stamp .digital-stamp-body {
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: space-between;

        border: 0.125rem solid #80b8d3;
        border-radius: 0.375rem;
        text-align: left;
        }

        .digital-stamp .digital-stamp-name {
        flex: 0 1 auto;
        padding: 0.3125rem 0.3125rem 0.3125rem 0.625rem;
        }

        .digital-stamp .digital-stamp-data {
        flex: 0 1 auto;
        padding: 0.3125rem 0.625rem 0.3125rem 0.3125rem;
        }
        .digital-stamp .digital-stamp-name-text {
        font-size: 1.125rem;
        font-weight: bold;
        line-height: 1;
        max-width: 9rem;
        word-wrap: break-word;
        }
        .digital-stamp .digital-stamp-data-text {
        font-size: 0.625rem;
        line-height: 1;
        max-width: 6rem;
        word-wrap: break-word;
        }

      </style>
    </head>
  </xsl:template>

  <!-- Base Templates -->

  <xsl:template name="DocumentSignatures">
    <xsl:param name="Signatures"/>
    <xsl:if test="$Signatures">
      <xsl:for-each select="$Signatures/Signature">
        <xsl:call-template name="DocumentSignature">
          <xsl:with-param name="Signature" select = "$Signatures/Signature" />
        </xsl:call-template>
      </xsl:for-each>
    </xsl:if>
  </xsl:template>

  <xsl:template name="DocumentSignature">
    <xsl:param name="Signature"/>
    <xsl:if test="$Signature">
      <div class="digital-stamp">
        <div class="digital-stamp-body">
          <div class="digital-stamp-name">
            <div class="digital-stamp-name-text">
              <xsl:value-of select="xslExtension:ExtractFirstGroup(SigningCertificateData/Subject/., 'CN=(.+?),')"/>
            </div>
          </div>
          <div class="digital-stamp-data">
            <div class="digital-stamp-data-text">
              Digitally signed by<br/>
              <xsl:value-of select="xslExtension:ExtractFirstGroup(SigningCertificateData/Subject/., 'CN=(.+?),')"/><br/>
              Date:&#32;
              <xsl:value-of disable-output-escaping="yes" select="xslExtension:FormatDate(SignatureTime/., 'yyyy.MM.dd&lt;\/br&gt;HH:mm:ss zzz')"/>
            </div>
          </div>
        </div>
      </div>
    </xsl:if>
  </xsl:template>

  <xsl:template name="PersonNames">
    <xsl:param name="Names"/>
    <xsl:value-of  select="$Names/NM:First/."/>&#160;<xsl:choose>
      <xsl:when test="$Names/NM:Middle/.">
        <xsl:value-of  select="$Names/NM:Middle/."/>&#160;
      </xsl:when>
    </xsl:choose><xsl:value-of  select="$Names/NM:Last/."/>
  </xsl:template>

  <xsl:template name="IdentityDocument">
    <xsl:param name="IdentityDocument"/>
    <xsl:choose>
      <xsl:when test="$IdentityDocument">
        <xsl:call-template name="IdentityDocumentName">
          <xsl:with-param name="IdentityDocument" select = "$IdentityDocument" />
        </xsl:call-template>№:&#160;<xsl:value-of  select="$IdentityDocument/IDBD:IdentityNumber"/>,

        изд. на:&#160;<xsl:choose>
          <xsl:when test="$IdentityDocument/IDBD:IdentitityIssueDate">
            <xsl:value-of select="xslExtension:FormatDate($IdentityDocument/IDBD:IdentitityIssueDate, 'dd.MM.yyyy')"/>г.
          </xsl:when>
        </xsl:choose>&#160;от:&#160;<xsl:value-of  select="$IdentityDocument/IDBD:IdentityIssuer"/>

      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="IdentityDocumentName">
    <xsl:param name="IdentityDocument"/>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
        Лична карта
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
        Паспорт
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
        Дипломатически паспорт
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
        Служебен паспорт
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
        Моряшки паспорт
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
        Военна карта за самоличност
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
        Свидетелство за управление на моторно превозно средство
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
        Временен паспорт
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
        Служебен открит лист за преминаване на границата
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
        Карта на бежанец
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
        Карта на чужденец с хуманитарен статут
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
        Карта на чужденец, получил убежище
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
        Разрешение за пребиваване
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
        Удостоверение за пребиваване на гражданин на ЕС
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="Declarations">
    <xsl:param name="Declarations"/>
    <xsl:param name="Declaration"/>

    <xsl:choose>
      <xsl:when test = "$Declarations">
        <xsl:for-each select="$Declaration">
          <xsl:call-template name="Declaration">
            <xsl:with-param name="Declaration" select = "." />
          </xsl:call-template>
        </xsl:for-each>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="Declaration">
    <xsl:param name="Declaration"/>
    <xsl:choose>
      <xsl:when test="$Declaration/DECL:IsDeclarationFilled = 'true'">
        <tr>
          <td colspan="2">
            <xsl:value-of  select="$Declaration/DECL:DeclarationName" disable-output-escaping="yes"/>
          </td>
        </tr>
        <xsl:choose>
          <xsl:when test="$Declaration/DECL:FurtherDescriptionFromDeclarer">
            <tr>
              <td colspan="2">
                Декларирам (допълнително описание на обстоятелствата по декларацията):<xsl:value-of  select="$Declaration/DECL:FurtherDescriptionFromDeclarer"/>
              </td>
            </tr>
            <tr>
              <td colspan="2">
                <xsl:value-of  select="$Declaration/DECL:FurtherDescriptionFromDeclarer"/>
              </td>
            </tr>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="AttachedDocuments">
    <xsl:param name="AttachedDocuments"/>
    <xsl:param name="AttachedDocument"/>
    <xsl:choose>
      <xsl:when test = "$AttachedDocuments">
        <tr>
          <td colspan="2">
            Приложени документи:
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <ol>
              <xsl:for-each select="$AttachedDocument">
                <li>
                  <xsl:value-of select="ADD:AttachedDocumentDescription" />
                </li>
              </xsl:for-each>
            </ol>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ApplicationElectronicAdministrativeServiceFooter">
    <xsl:param name="ElectronicAdministrativeServiceFooter"/>
    <xsl:param name="SignatureXML"/>

    <tr>
      <td width="50%">
        <xsl:call-template name="ElectronicAdministrativeServiceFooterDate">
          <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "$ElectronicAdministrativeServiceFooter" />
        </xsl:call-template>
      </td>
      <td width="50%">
        <xsl:call-template name="DocumentSignatures">
          <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
        </xsl:call-template>
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="ElectronicAdministrativeServiceFooterDate">
    <xsl:param name="ElectronicAdministrativeServiceFooter"/>
    Дата:&#160;<xsl:call-template name="Date">
      <xsl:with-param name="Date" select = "$ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime" />
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="Date">
    <xsl:param name="Date"/>
    <xsl:choose>
      <xsl:when test="$Date">
        <xsl:value-of select="xslExtension:FormatDate($Date, 'dd.MM.yyyy')"/>г.
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ContractAssignor">
    <xsl:param name="ContractAssignor"/>
    <xsl:choose>
      <xsl:when test = "$ContractAssignor">
        <tr>
          <td colspan="2">
            Тип възложител:&#160;
            <xsl:if test="$ContractAssignor/ca:AssignorPersonEntityType/. = 1">
              физическо лице<br/>
              <xsl:call-template name="PersonAssignorData">
                <xsl:with-param name="PersonAssignorData" select = "$ContractAssignor/ca:PersonAssignorData" />
              </xsl:call-template>
            </xsl:if>
            <xsl:if test="$ContractAssignor/ca:AssignorPersonEntityType/. = 2">
              юридическо лице<br/>
              <xsl:call-template name="EntityAssignorData">
                <xsl:with-param name="EntityAssignorData" select = "$ContractAssignor/ca:EntityAssignorData" />
              </xsl:call-template>
            </xsl:if>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="PersonAssignorData">
    <xsl:param name="PersonAssignorData"/>
    <xsl:choose>
      <xsl:when test = "$PersonAssignorData">
        <xsl:value-of select="$PersonAssignorData/pad:FullName" />,&#160;
        <xsl:if test="$PersonAssignorData/pad:IdentifierType/. = '1'">
          ЕГН&#160;
        </xsl:if>
        <xsl:if test="$PersonAssignorData/pad:IdentifierType/. = '2'">
          ЛНЧ&#160;
        </xsl:if>
        <xsl:if test="$PersonAssignorData/pad:IdentifierType/. = '3'">
          ДДММГГГГ&#160;
        </xsl:if>
        <xsl:value-of select="$PersonAssignorData/pad:Identifier" /><xsl:if test="$PersonAssignorData/pad:Citizenship/. = '1'">
          ,&#160;Български гражданин&#160;
        </xsl:if>
        <xsl:if test="$PersonAssignorData/pad:Citizenship/. = '2'">
          ,&#160;Чужд гражданин&#160;
        </xsl:if>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="GuardedPersonData">
    <xsl:param name="GuardedPersonData"/>
    <xsl:param name="ForTermination"/>
    <xsl:choose>
      <xsl:when test = "$GuardedPersonData">
        <xsl:value-of select="$GuardedPersonData/pad:FullName" />,
        ЕГН/ЛН/ЛНЧ/ДДММГГГГ&#160;<xsl:value-of select="$GuardedPersonData/pad:Identifier" /><xsl:choose>
          <xsl:when test = "$ForTermination = 0">, охранявано лице&#160;
          </xsl:when>
        </xsl:choose>
        
        <xsl:if test="$GuardedPersonData/pad:GuardedType/. = '1'">
          Мъж
        </xsl:if>
        <xsl:if test="$GuardedPersonData/pad:GuardedType/. = '2'">
          Жена
        </xsl:if>
        <xsl:if test="$GuardedPersonData/pad:GuardedType/. = '3'">
          Момче
        </xsl:if>
        <xsl:if test="$GuardedPersonData/pad:GuardedType/. = '4'">
          Момиче
        </xsl:if>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="EntityAssignorData">
    <xsl:param name="EntityAssignorData"/>
    <xsl:choose>
      <xsl:when test = "$EntityAssignorData">
        <xsl:value-of select="$EntityAssignorData/ead:FullName" />,
        ЕИК&#160;  <xsl:value-of select="$EntityAssignorData/ead:Identifier" />
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="SecurityObject">
    <xsl:param name="ForTermination"/>
    <xsl:call-template name="PointOfPrivateSecurityServicesLaw">
      <xsl:with-param name="PointOfPrivateSecurityServicesLaw" select = "so:PointOfPrivateSecurityServicesLaw/." />
    </xsl:call-template>
    <br/>
    <xsl:call-template name="PersonalSecurity">
      <xsl:with-param name="PersonalSecurity" select = "so:PersonalSecurity" />
      <xsl:with-param name="ForTermination" select = "$ForTermination" />
    </xsl:call-template>
    <xsl:call-template name="ProtectionPersonsProperty">
      <xsl:with-param name="ProtectionPersonsProperty" select = "so:ProtectionPersonsProperty" />
      <xsl:with-param name="ForTermination" select = "$ForTermination" />
    </xsl:call-template>
    <xsl:call-template name="AlarmAndSecurityActivity">
      <xsl:with-param name="AlarmAndSecurityActivity" select = "so:AlarmAndSecurityActivity" />
      <xsl:with-param name="ForTermination" select = "$ForTermination" />
    </xsl:call-template>
    <xsl:call-template name="SecurityOfEvents">
      <xsl:with-param name="SecurityOfEvents" select = "so:SecurityOfEvents" />
      <xsl:with-param name="ForTermination" select = "$ForTermination" />
    </xsl:call-template>
    <xsl:call-template name="SecurityTransportingCargo">
      <xsl:with-param name="SecurityTransportingCargo" select = "so:SecurityTransportingCargo" />
      <xsl:with-param name="ForTermination" select = "$ForTermination" />
    </xsl:call-template>
    <xsl:call-template name="ProtectionOfAgriculturalProperty">
      <xsl:with-param name="ProtectionOfAgriculturalProperty" select = "so:ProtectionOfAgriculturalProperty" />
      <xsl:with-param name="ForTermination" select = "$ForTermination" />
    </xsl:call-template>
    <xsl:call-template name="SelfProtectionPersonsProperty">
      <xsl:with-param name="SelfProtectionPersonsProperty" select = "so:SelfProtectionPersonsProperty" />
      <xsl:with-param name="ForTermination" select = "$ForTermination" />
    </xsl:call-template>
    <xsl:call-template name="SecurityOfSitesRealEstate">
      <xsl:with-param name="SecurityOfSitesRealEstate" select = "so:SecurityOfSitesRealEstate" />
      <xsl:with-param name="ForTermination" select = "$ForTermination" />
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="PointOfPrivateSecurityServicesLaw">
    <xsl:param name="PointOfPrivateSecurityServicesLaw"/>
    <xsl:choose>
      <xsl:when test = "$PointOfPrivateSecurityServicesLaw = 'R-301'">
        Охрана на обект по чл. 5, ал. 1, т. 1 от ЗЧОД - лична охрана на физически лица.
      </xsl:when>
      <xsl:when test = "$PointOfPrivateSecurityServicesLaw = 'R-302'">
        Охрана на обект по чл. 5, ал. 1, т. 2 от ЗЧОД - охрана на имущество на физически или юридически лица
      </xsl:when>
      <xsl:when test = "$PointOfPrivateSecurityServicesLaw = 'R-307'">
        Охрана на обект по чл. 5, ал. 1, т. 3 от ЗЧОД - сигнално-охранителна дейност
      </xsl:when>
      <!--<xsl:when test = "$PointOfPrivateSecurityServicesLaw = 'R-305'">
        Охрана на обект по чл. 5, ал. 1, т. 4 от ЗЧОД - самоохрана на имущество на търговци или юридически лица
      </xsl:when>-->
      <xsl:when test = "$PointOfPrivateSecurityServicesLaw = 'R-308'">
        Охрана на обект по чл. 5, ал. 1, т. 5 от ЗЧОД - охрана на обекти - недвижими имоти
      </xsl:when>
      <xsl:when test = "$PointOfPrivateSecurityServicesLaw = 'R-303'">
        Охрана на обект по чл. 5, ал. 1, т. 7 от ЗЧОД - охрана на мероприятия
      </xsl:when>
      <xsl:when test = "$PointOfPrivateSecurityServicesLaw = 'R-304'">
        Охрана на обект по чл. 5, ал. 1, т. 8 от ЗЧОД - охрана при транспортиране на ценни пратки или товари
      </xsl:when>
      <xsl:when test = "$PointOfPrivateSecurityServicesLaw = 'R-309'">
        Охрана на обект по чл. 5, ал. 1, т. 9 от ЗЧОД - охрана на селскостопанско имущество
      </xsl:when>
    </xsl:choose>

  </xsl:template>

  <xsl:template name="SecurityTransport">
    &#160;&#160;&#160;- Собственост на транспортното средство -
    <xsl:if test="st:VehicleOwnershipType/. = 1">
      Собствено
    </xsl:if>
    <xsl:if test="st:VehicleOwnershipType/. = 2">
      Наето
    </xsl:if>
    <br/>
    &#160;&#160;&#160;- Регистрационен номер -  <xsl:value-of select="st:RegistrationNumber/." /><br/>
    &#160;&#160;&#160;- Марка и модел -  <xsl:value-of select="st:MakeAndModel/." /><br/>
  </xsl:template>

  <xsl:template name="SecurityType">
    <xsl:param name="SecurityType"/>
    <xsl:if test="$SecurityType/. = 1">
      Въоръжена
    </xsl:if>
    <xsl:if test="$SecurityType/. = 2">
      Невъоръжена
    </xsl:if>
  </xsl:template>

  <xsl:template name="AccessRegime">
    <xsl:param name="AccessRegimeType"/>
    <xsl:if test="$AccessRegimeType/. = 2">
      Извършване на пропускателен режим
    </xsl:if>
    <xsl:if test="$AccessRegimeType/. = 3">
      Неизвършване на пропускателен режим
    </xsl:if>
    <br/>
  </xsl:template>

  <xsl:template name="Control">
    <xsl:param name="ControlType"/>
    <xsl:if test="$ControlType/. = 4">
      Видеонаблюдение
    </xsl:if>
    <xsl:if test="$ControlType/. = 5">
      Мониторен контрол
    </xsl:if>
    <br/>
  </xsl:template>

  <xsl:template name="Clothint">
    <xsl:param name="ClothintType"/>
    <xsl:if test="$ClothintType/. = 1">
      Ежедневно
    </xsl:if>
    <xsl:if test="$ClothintType/. = 2">
      Униформено
    </xsl:if>
  </xsl:template>

  <xsl:template name="ProtectionPersonsProperty">
    <xsl:param name="ProtectionPersonsProperty"/>
    <xsl:param name="ForTermination"/>
    <xsl:choose>
      <xsl:when test = "$ProtectionPersonsProperty">
        <xsl:choose>
          <xsl:when test = "$ForTermination = 0">
            Наименование на охранявания обект:&#160;<xsl:value-of select="$ProtectionPersonsProperty/ppp:SecurityObjectName" />,<br/>
            <xsl:choose>
              <xsl:when test="$ProtectionPersonsProperty/ppp:DistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$ProtectionPersonsProperty/ppp:DistrictName" />,<br/>
              </xsl:when>
              <xsl:when test="$ProtectionPersonsProperty/ppp:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$ProtectionPersonsProperty/ppp:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose>
            Адрес на охранявания обект:&#160;<xsl:value-of select="$ProtectionPersonsProperty/ppp:Address" /><br/>
            <br/>
            Средства за охрана:<br/>
            ОРЪЖИЕ<br/>
            &#160;&#160;&#160;- Вид на охраната -
            <xsl:call-template name="SecurityType">
              <xsl:with-param name="SecurityType" select = "$ProtectionPersonsProperty/ppp:SecurityType" />
            </xsl:call-template><br/>
            <xsl:if test="$ProtectionPersonsProperty/ppp:SecurityTransports">
              ТРАНСПОРТ<br/>
              <xsl:for-each select="$ProtectionPersonsProperty/ppp:SecurityTransports/ppp:SecurityTransport">
                <xsl:call-template name="SecurityTransport">
                </xsl:call-template>
                <br/>
              </xsl:for-each>
            </xsl:if>
            <xsl:choose>
              <xsl:when test="$ProtectionPersonsProperty/ppp:AISCHODAccessRegimeTypeName">
                <xsl:value-of select="$ProtectionPersonsProperty/ppp:AISCHODAccessRegimeTypeName" />
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$ProtectionPersonsProperty/ppp:AccessRegimeType">
                <xsl:call-template name="AccessRegime">
                  <xsl:with-param name="AccessRegimeType" select = "$ProtectionPersonsProperty/ppp:AccessRegimeType" />
                </xsl:call-template>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$ProtectionPersonsProperty/ppp:AISCHODControlTypeName">
                <xsl:value-of select="$ProtectionPersonsProperty/ppp:AISCHODControlTypeName" />
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$ProtectionPersonsProperty/ppp:ControlType">
                <xsl:call-template name="Control">
                  <xsl:with-param name="ControlType" select = "$ProtectionPersonsProperty/ppp:ControlType" />
                </xsl:call-template>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото поемане на охраната &#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$ProtectionPersonsProperty/ppp:ActualDate" />
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            Наименование на охранявания обект:&#160;<xsl:value-of select="$ProtectionPersonsProperty/ppp:SecurityObjectName" />,<br/>
            <xsl:choose>
              <xsl:when test="$ProtectionPersonsProperty/ppp:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$ProtectionPersonsProperty/ppp:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$ProtectionPersonsProperty/ppp:Address">
                Адрес на охранявания обект:&#160;<xsl:value-of select="$ProtectionPersonsProperty/ppp:Address" /><br/>
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$ProtectionPersonsProperty/ppp:ContractTypeNumberDate">
                Вид, номер и дата на документа за прекратяване<xsl:value-of select="$ProtectionPersonsProperty/ppp:ContractTypeNumberDate" /><br/>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото прекратяване на охраната&#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$ProtectionPersonsProperty/ppp:TerminationDate" />
            </xsl:call-template>
            <xsl:choose>
              <xsl:when test="$ProtectionPersonsProperty/ppp:ContractTerminationNote">
                <br/>Забележка:&#160;<xsl:value-of  select="$ProtectionPersonsProperty/ppp:ContractTerminationNote"/>,
              </xsl:when>
            </xsl:choose>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="PersonalSecurity">
    <xsl:param name="PersonalSecurity"/>
    <xsl:param name="ForTermination"/>
    <xsl:choose>
      <xsl:when test = "$PersonalSecurity">
        <xsl:choose>
          <xsl:when test = "$ForTermination = 0">
            Имена на охраняваното лице:
            <xsl:call-template name="GuardedPersonData">
              <xsl:with-param name="GuardedPersonData" select = "$PersonalSecurity/ps:GuardedPerson" />
              <xsl:with-param name="ForTermination" select = "$ForTermination" />
            </xsl:call-template>, длъжност&#160;<xsl:value-of select="$PersonalSecurity/ps:Position" />
            , месторабота&#160;<xsl:value-of select="$PersonalSecurity/ps:PlaceOfWork" />
            , адрес на охраняваното лице&#160;<xsl:value-of select="$PersonalSecurity/ps:AISCHODDistrictName" />, <xsl:value-of select="$PersonalSecurity/ps:Address" />
            , възложителят е&#160;
            <xsl:if test="$PersonalSecurity/ps:GuardedPersonType/. = '1'">
              охраняваното лице
            </xsl:if>
            <xsl:if test="$PersonalSecurity/ps:GuardedPersonType/. = '2'">
              представител
            </xsl:if>
            <br/> <br/>
            Средства за охрана:<br/>
            ОРЪЖИЕ<br/>
            &#160;&#160;&#160;- Вид на охраната -
            <xsl:call-template name="SecurityType">
              <xsl:with-param name="SecurityType" select = "$PersonalSecurity/ps:SecurityType" />
            </xsl:call-template><br/>
            &#160;&#160;&#160;- Вид на облеклото на охранителния състав –
            <xsl:call-template name="Clothint">
              <xsl:with-param name="ClothintType" select = "$PersonalSecurity/ps:ClothintType" />
            </xsl:call-template><br/>
            ТРАНСПОРТ<br/>
            <xsl:for-each select="$PersonalSecurity/ps:SecurityTransports/ps:SecurityTransport">
              <xsl:call-template name="SecurityTransport">
              </xsl:call-template>
              <br/>
            </xsl:for-each>
            Дата на фактическото поемане на охраната &#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$PersonalSecurity/ps:ActualDate" />
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            Имена на охраняваното лице:
            <xsl:call-template name="GuardedPersonData">
              <xsl:with-param name="GuardedPersonData" select = "$PersonalSecurity/ps:GuardedPerson" />
              <xsl:with-param name="ForTermination" select = "$ForTermination" />
            </xsl:call-template> <br/>
            <xsl:choose>
              <xsl:when test="$PersonalSecurity/ps:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$PersonalSecurity/ps:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose><br/> <br/>
            <xsl:choose>
              <xsl:when test="$PersonalSecurity/ps:ContractTypeNumberDate">
                Вид, номер и дата на документа за прекратяване<xsl:value-of select="$PersonalSecurity/ps:ContractTypeNumberDate" /><br/>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото прекратяване на охраната&#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$PersonalSecurity/ps:TerminationDate" />
            </xsl:call-template>
            <xsl:choose>
              <xsl:when test="$PersonalSecurity/ps:ContractTerminationNote">
                <br/>Забележка:&#160;<xsl:value-of  select="$PersonalSecurity/ps:ContractTerminationNote"/>,
              </xsl:when>
            </xsl:choose>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="AlarmAndSecurityActivity">
    <xsl:param name="AlarmAndSecurityActivity"/>
    <xsl:param name="ForTermination"/>
    <xsl:choose>
      <xsl:when test = "$AlarmAndSecurityActivity">
        <xsl:choose>
          <xsl:when test = "$ForTermination = 0">
            Наименование на охранявания обект:&#160;<xsl:value-of select="$AlarmAndSecurityActivity/aasa:SecurityObjectName" />,<br/>
            <xsl:choose>
              <xsl:when test="$AlarmAndSecurityActivity/aasa:DistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$AlarmAndSecurityActivity/aasa:DistrictName" />,<br/>
              </xsl:when>
              <xsl:when test="$AlarmAndSecurityActivity/aasa:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$AlarmAndSecurityActivity/aasa:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose>
            Адрес на охранявания обект:&#160;<xsl:value-of select="$AlarmAndSecurityActivity/aasa:Address" /><br/>
            <br/>
            <xsl:if test="$AlarmAndSecurityActivity/aasa:SecurityType or $AlarmAndSecurityActivity/aasa:SecurityTransports">
              Средства за охрана:<br/>
            </xsl:if>
            <xsl:if test="$AlarmAndSecurityActivity/aasa:SecurityType">
              ОРЪЖИЕ<br/>
              &#160;&#160;&#160;- Вид на охраната -
              <xsl:call-template name="SecurityType">
                <xsl:with-param name="SecurityType" select = "$AlarmAndSecurityActivity/aasa:SecurityType" />
              </xsl:call-template><br/>
            </xsl:if>
            <xsl:if test="$AlarmAndSecurityActivity/aasa:SecurityTransports">
              ТРАНСПОРТ<br/>
              <xsl:for-each select="$AlarmAndSecurityActivity/aasa:SecurityTransports/aasa:SecurityTransport">
                <xsl:call-template name="SecurityTransport">
                </xsl:call-template>
                <br/>
              </xsl:for-each>
            </xsl:if>
            Дата на фактическото поемане на охраната &#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$AlarmAndSecurityActivity/aasa:ActualDate" />
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            Наименование на охранявания обект:&#160;<xsl:value-of select="$AlarmAndSecurityActivity/aasa:SecurityObjectName" />,<br/>
            <xsl:choose>
              <xsl:when test="$AlarmAndSecurityActivity/aasa:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$AlarmAndSecurityActivity/aasa:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$AlarmAndSecurityActivity/aasa:Address">
                Адрес на охранявания обект:&#160;<xsl:value-of select="$AlarmAndSecurityActivity/aasa:Address" /><br/>
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$AlarmAndSecurityActivity/aasa:ContractTypeNumberDate">
                Вид, номер и дата на документа за прекратяване<xsl:value-of select="$AlarmAndSecurityActivity/aasa:ContractTypeNumberDate" /><br/>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото прекратяване на охраната&#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$AlarmAndSecurityActivity/aasa:TerminationDate" />
            </xsl:call-template>
            <xsl:choose>
              <xsl:when test="$AlarmAndSecurityActivity/aasa:ContractTerminationNote">
                <br/>Забележка:&#160;<xsl:value-of  select="$AlarmAndSecurityActivity/aasa:ContractTerminationNote"/>,
              </xsl:when>
            </xsl:choose>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="SecurityOfEvents">
    <xsl:param name="SecurityOfEvents"/>
    <xsl:param name="ForTermination"/>
    <xsl:choose>
      <xsl:when test = "$SecurityOfEvents">
        <xsl:choose>
          <xsl:when test = "$ForTermination = 0">
            Наименование на охранявания обект:&#160;<xsl:value-of select="$SecurityOfEvents/soe:SecurityObjectName" />,<br/>
            <xsl:choose>
              <xsl:when test="$SecurityOfEvents/soe:DistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$SecurityOfEvents/soe:DistrictName" />,<br/>
              </xsl:when>
              <xsl:when test="$SecurityOfEvents/soe:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$SecurityOfEvents/soe:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose>
            Адрес на охранявания обект:&#160;<xsl:value-of select="$SecurityOfEvents/soe:Address" /><br/>
            <br/>
            <xsl:if test="$SecurityOfEvents/soe:SecurityType or $SecurityOfEvents/soe:SecurityTransports">
              Средства за охрана:<br/>
            </xsl:if>
            <xsl:if test="$SecurityOfEvents/soe:SecurityType">
              ОРЪЖИЕ<br/>
              &#160;&#160;&#160;- Вид на охраната -
              <xsl:call-template name="SecurityType">
                <xsl:with-param name="SecurityType" select = "$SecurityOfEvents/soe:SecurityType" />
              </xsl:call-template><br/>
            </xsl:if>
            <xsl:if test="$SecurityOfEvents/soe:SecurityTransports">
              ТРАНСПОРТ<br/>
              <xsl:for-each select="$SecurityOfEvents/soe:SecurityTransports/soe:SecurityTransport">
                <xsl:call-template name="SecurityTransport">
                </xsl:call-template>
                <br/>
              </xsl:for-each>
            </xsl:if>
            <xsl:choose>
              <xsl:when test="$SecurityOfEvents/soe:AISCHODAccessRegimeTypeName">
                <xsl:value-of select="$SecurityOfEvents/soe:AISCHODAccessRegimeTypeName" />
                <br/>
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SecurityOfEvents/soe:AISCHODControlTypeName">
                <xsl:value-of select="$SecurityOfEvents/soe:AISCHODControlTypeName" />
                <br/>
                <br/>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото поемане на охраната &#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$SecurityOfEvents/soe:ActualDate" />
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            Наименование на охранявания обект:&#160;<xsl:value-of select="$SecurityOfEvents/soe:SecurityObjectName" />,<br/>
            <xsl:choose>
              <xsl:when test="$SecurityOfEvents/soe:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$SecurityOfEvents/soe:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SecurityOfEvents/soe:Address">
                Адрес на охранявания обект:&#160;<xsl:value-of select="$SecurityOfEvents/soe:Address" /><br/>
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SecurityOfEvents/soe:ContractTypeNumberDate">
                Вид, номер и дата на документа за прекратяване<xsl:value-of select="$SecurityOfEvents/soe:ContractTypeNumberDate" /><br/>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото прекратяване на охраната&#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$SecurityOfEvents/soe:TerminationDate" />
            </xsl:call-template>
            <xsl:choose>
              <xsl:when test="$SecurityOfEvents/soe:ContractTerminationNote">
                <br/>Забележка:&#160;<xsl:value-of  select="$SecurityOfEvents/soe:ContractTerminationNote"/>,
              </xsl:when>
            </xsl:choose>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

	<xsl:template name="SecurityTransportingCargo">
		<xsl:param name="SecurityTransportingCargo"/>
		<xsl:param name="ForTermination"/>
		<xsl:if test="$SecurityTransportingCargo/stc:ObjectTypes">
			Вид обекти, от и към които ще се извършва транспортирането:&#160;<xsl:value-of select="$SecurityTransportingCargo/stc:ObjectTypes" /><br/>
		</xsl:if>
		<xsl:if test="$SecurityTransportingCargo/stc:TerritorialScopeFrom">
			Териториален обхват на транспортирането от&#160;<xsl:value-of select="$SecurityTransportingCargo/stc:TerritorialScopeFrom" /><br/>
		</xsl:if>
		<xsl:if test="$SecurityTransportingCargo/stc:TerritorialScopeTo">
			Териториален обхват на транспортирането до&#160;<xsl:value-of select="$SecurityTransportingCargo/stc:TerritorialScopeTo" /><br/><br/>
		</xsl:if>
		<xsl:if test="$SecurityTransportingCargo/stc:SecurityType">
			ОРЪЖИЕ<br/>
			&#160;&#160;&#160;- Вид на охраната -
			<xsl:call-template name="SecurityType">
				<xsl:with-param name="SecurityType" select = "$SecurityTransportingCargo/stc:SecurityType" />
			</xsl:call-template><br/>
		</xsl:if>
		<xsl:if test="$SecurityTransportingCargo/stc:SecurityTransports/stc:SecurityTransport">
			ТРАНСПОРТ<br/>
			<xsl:for-each select="$SecurityTransportingCargo/stc:SecurityTransports/stc:SecurityTransport">
				<xsl:call-template name="SecurityTransport">
				</xsl:call-template>
				<br/>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="$SecurityTransportingCargo/stc:ActualDate">
			Дата на фактическото поемане на охраната &#160;
			<xsl:call-template name="Date">
				<xsl:with-param name="Date" select = "$SecurityTransportingCargo/stc:ActualDate" />
			</xsl:call-template>
		</xsl:if>
		<xsl:if test="$SecurityTransportingCargo/stc:ContractTypeNumberDate">
			Вид, номер и дата на документа за прекратяване:&#160;<xsl:value-of select="$SecurityTransportingCargo/stc:ContractTypeNumberDate" /><br/>
		</xsl:if>
		<xsl:if test="$SecurityTransportingCargo/stc:TerminationDate">
			Дата на фактическото прекратяване на охраната&#160;
			<xsl:call-template name="Date">
				<xsl:with-param name="Date" select = "$SecurityTransportingCargo/stc:TerminationDate" />
			</xsl:call-template>
		</xsl:if>
		<xsl:if test="$SecurityTransportingCargo/stc:ContractTerminationNote">
			<br/>Забележка:&#160;<xsl:value-of  select="$SecurityTransportingCargo/stc:ContractTerminationNote"/>
		</xsl:if>
	</xsl:template>
	
  <xsl:template name="ProtectionOfAgriculturalProperty">
    <xsl:param name="ProtectionOfAgriculturalProperty"/>
    <xsl:param name="ForTermination"/>
    <xsl:choose>
      <xsl:when test = "$ProtectionOfAgriculturalProperty">
        <xsl:choose>
          <xsl:when test = "$ForTermination = 0">
            Наименование на охранявания обект:&#160;<xsl:value-of select="$ProtectionOfAgriculturalProperty/poap:SecurityObjectName" />,<br/>
            <xsl:choose>
              <xsl:when test="$ProtectionOfAgriculturalProperty/poap:DistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$ProtectionOfAgriculturalProperty/poap:DistrictName" />,<br/>
              </xsl:when>
              <xsl:when test="$ProtectionOfAgriculturalProperty/poap:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$ProtectionOfAgriculturalProperty/poap:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose>
            Адрес на охранявания обект:&#160;<xsl:value-of select="$ProtectionOfAgriculturalProperty/poap:Address" /><br/>
            <br/>
            Средства за охрана:<br/>
            ОРЪЖИЕ<br/>
            &#160;&#160;&#160;- Вид на охраната -
            <xsl:call-template name="SecurityType">
              <xsl:with-param name="SecurityType" select = "$ProtectionOfAgriculturalProperty/poap:SecurityType" />
            </xsl:call-template><br/>
            ТРАНСПОРТ<br/>
            <xsl:for-each select="$ProtectionOfAgriculturalProperty/poap:SecurityTransports/poap:SecurityTransport">
              <xsl:call-template name="SecurityTransport">
              </xsl:call-template>
              <br/>
            </xsl:for-each>
            Дата на фактическото поемане на охраната &#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$ProtectionOfAgriculturalProperty/poap:ActualDate" />
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            Наименование на охранявания обект:&#160;<xsl:value-of select="$ProtectionOfAgriculturalProperty/poap:SecurityObjectName" />,<br/>
            <xsl:choose>
              <xsl:when test="$ProtectionOfAgriculturalProperty/poap:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$ProtectionOfAgriculturalProperty/poap:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$ProtectionOfAgriculturalProperty/poap:Address">
                Адрес на охранявания обект:&#160;<xsl:value-of select="$ProtectionOfAgriculturalProperty/poap:Address" /><br/>
                <br/>
              </xsl:when>
            </xsl:choose>
            <br/>
            <xsl:choose>
              <xsl:when test="$ProtectionOfAgriculturalProperty/poap:ContractTypeNumberDate">
                Вид, номер и дата на документа за прекратяване<xsl:value-of select="$ProtectionOfAgriculturalProperty/poap:ContractTypeNumberDate" /><br/>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото прекратяване на охраната&#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$ProtectionOfAgriculturalProperty/poap:TerminationDate" />
            </xsl:call-template>
            <xsl:choose>
              <xsl:when test="$ProtectionOfAgriculturalProperty/poap:ContractTerminationNote">
                <br/>Забележка:&#160;<xsl:value-of  select="$ProtectionOfAgriculturalProperty/poap:ContractTerminationNote"/>,
              </xsl:when>
            </xsl:choose>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="SelfProtectionPersonsProperty">
    <xsl:param name="SelfProtectionPersonsProperty"/>
    <xsl:param name="ForTermination"/>
    <xsl:choose>
      <xsl:when test = "$SelfProtectionPersonsProperty">
        Наименование на охранявания обект:&#160;<xsl:value-of select="$SelfProtectionPersonsProperty/sppp:SecurityObjectName" />,<br/>
        <xsl:choose>
          <xsl:when test="$SelfProtectionPersonsProperty/sppp:AISCHODDistrictName">
            Област в която се намира обекта:&#160;<xsl:value-of select="$SelfProtectionPersonsProperty/sppp:AISCHODDistrictName" />,<br/>
          </xsl:when>
        </xsl:choose>
        <xsl:choose>
          <xsl:when test="$SelfProtectionPersonsProperty/sppp:Address">
            Адрес на охранявания обект:&#160;<xsl:value-of select="$SelfProtectionPersonsProperty/sppp:Address" /><br/>
            <br/>
          </xsl:when>
        </xsl:choose>
        <xsl:choose>
          <xsl:when test = "$ForTermination = 0">
            Средства за охрана:<br/>
            ОРЪЖИЕ<br/>
            &#160;&#160;&#160;- Вид на охраната -
            <xsl:call-template name="SecurityType">
              <xsl:with-param name="SecurityType" select = "$SelfProtectionPersonsProperty/sppp:SecurityType" />
            </xsl:call-template><br/>
            ТРАНСПОРТ<br/>
            <xsl:if test="$SelfProtectionPersonsProperty/sppp:HasTransport/. = 'false'">
              &#160;&#160;&#160; Няма<br/><br/>
            </xsl:if>
            <xsl:if test="$SelfProtectionPersonsProperty/sppp:HasTransport/. = 'true'">
              <xsl:for-each select="$SelfProtectionPersonsProperty/sppp:SecurityTransports/sppp:SecurityTransport">
                <xsl:call-template name="SecurityTransport">
                </xsl:call-template>
                <br/>
              </xsl:for-each>
            </xsl:if>
            <xsl:choose>
              <xsl:when test="$SelfProtectionPersonsProperty/sppp:AISCHODAccessRegimeTypeName">
                <xsl:value-of select="$SelfProtectionPersonsProperty/sppp:AISCHODAccessRegimeTypeName" />
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SelfProtectionPersonsProperty/sppp:AccessRegimeType">
                <xsl:call-template name="AccessRegime">
                  <xsl:with-param name="AccessRegimeType" select = "$SelfProtectionPersonsProperty/sppp:AccessRegimeType" />
                </xsl:call-template>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SelfProtectionPersonsProperty/sppp:AISCHODControlTypeName">
                <xsl:value-of select="$SelfProtectionPersonsProperty/sppp:AISCHODControlTypeName" />
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SelfProtectionPersonsProperty/sppp:ControlType">
                <xsl:call-template name="Control">
                  <xsl:with-param name="ControlType" select = "$SelfProtectionPersonsProperty/sppp:ControlType" />
                </xsl:call-template>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото поемане на охраната &#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$SelfProtectionPersonsProperty/sppp:ActualDate" />
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            <xsl:choose>
              <xsl:when test="$SelfProtectionPersonsProperty/sppp:ContractTypeNumberDate">
                Вид, номер и дата на документа за прекратяване<xsl:value-of select="$SelfProtectionPersonsProperty/sppp:ContractTypeNumberDate" /><br/>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото прекратяване на охраната&#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$SelfProtectionPersonsProperty/sppp:TerminationDate" />
            </xsl:call-template>
            <xsl:choose>
              <xsl:when test="$SelfProtectionPersonsProperty/sppp:ContractTerminationNote">
                <br/>Забележка:&#160;<xsl:value-of  select="$SelfProtectionPersonsProperty/sppp:ContractTerminationNote"/>,
              </xsl:when>
            </xsl:choose>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="SecurityOfSitesRealEstate">
    <xsl:param name="SecurityOfSitesRealEstate"/>
    <xsl:param name="ForTermination"/>
    <xsl:choose>
      <xsl:when test = "$SecurityOfSitesRealEstate">
        <xsl:choose>
          <xsl:when test = "$ForTermination = 0">
            Наименование на охранявания обект:&#160;<xsl:value-of select="$SecurityOfSitesRealEstate/sosre:SecurityObjectName" />,<br/>
            <xsl:choose>
              <xsl:when test="$SecurityOfSitesRealEstate/sosre:DistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$SecurityOfSitesRealEstate/sosre:DistrictName" />,<br/>
              </xsl:when>
              <xsl:when test="$SecurityOfSitesRealEstate/sosre:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$SecurityOfSitesRealEstate/sosre:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose>
            Адрес на охранявания обект:&#160;<xsl:value-of select="$SecurityOfSitesRealEstate/sosre:Address" /><br/>
            <br/>
            <xsl:if test="$SecurityOfSitesRealEstate/sosre:SecurityType or $SecurityOfSitesRealEstate/sosre:SecurityTransports">
              Средства за охрана:<br/>
            </xsl:if>
            <xsl:if test="$SecurityOfSitesRealEstate/sosre:SecurityType">
              ОРЪЖИЕ<br/>
              &#160;&#160;&#160;- Вид на охраната -
              <xsl:call-template name="SecurityType">
                <xsl:with-param name="SecurityType" select = "$SecurityOfSitesRealEstate/sosre:SecurityType" />
              </xsl:call-template><br/>
            </xsl:if>
            <xsl:if test="$SecurityOfSitesRealEstate/sosre:SecurityTransports">
              ТРАНСПОРТ<br/>
              <xsl:for-each select="$SecurityOfSitesRealEstate/sosre:SecurityTransports/sosre:SecurityTransport">
                <xsl:call-template name="SecurityTransport">
                </xsl:call-template>
                <br/>
              </xsl:for-each>
            </xsl:if>
            <xsl:choose>
              <xsl:when test="$SecurityOfSitesRealEstate/sosre:AISCHODAccessRegimeTypeName">
                <xsl:value-of select="$SecurityOfSitesRealEstate/sosre:AISCHODAccessRegimeTypeName" />
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SecurityOfSitesRealEstate/sosre:AccessRegimeType">
                <xsl:call-template name="AccessRegime">
                  <xsl:with-param name="AccessRegimeType" select = "$SecurityOfSitesRealEstate/sosre:AccessRegimeType" />
                </xsl:call-template>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SecurityOfSitesRealEstate/sosre:AISCHODControlTypeName">
                <xsl:value-of select="$SecurityOfSitesRealEstate/sosre:AISCHODControlTypeName" />
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SecurityOfSitesRealEstate/sosre:ControlType">
                <xsl:call-template name="Control">
                  <xsl:with-param name="ControlType" select = "$SecurityOfSitesRealEstate/sosre:ControlType" />
                </xsl:call-template>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото поемане на охраната &#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$SecurityOfSitesRealEstate/sosre:ActualDate" />
            </xsl:call-template>
          </xsl:when>
          <xsl:otherwise>
            Наименование на охранявания обект:&#160;<xsl:value-of select="$SecurityOfSitesRealEstate/sosre:SecurityObjectName" />,<br/>
            <xsl:choose>
              <xsl:when test="$SecurityOfSitesRealEstate/sosre:AISCHODDistrictName">
                Област в която се намира обекта:&#160;<xsl:value-of select="$SecurityOfSitesRealEstate/sosre:AISCHODDistrictName" />,<br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SecurityOfSitesRealEstate/sosre:Address">
                Адрес на охранявания обект:&#160;<xsl:value-of select="$SecurityOfSitesRealEstate/sosre:Address" /><br/>
                <br/>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="$SecurityOfSitesRealEstate/sosre:ContractTypeNumberDate">
                Вид, номер и дата на документа за прекратяване<xsl:value-of select="$SecurityOfSitesRealEstate/sosre:ContractTypeNumberDate" /><br/>
              </xsl:when>
            </xsl:choose>
            Дата на фактическото прекратяване на охраната&#160;
            <xsl:call-template name="Date">
              <xsl:with-param name="Date" select = "$SecurityOfSitesRealEstate/sosre:TerminationDate" />
            </xsl:call-template>
            <xsl:choose>
              <xsl:when test="$SecurityOfSitesRealEstate/sosre:ContractTerminationNote">
                <br/>Забележка:&#160;<xsl:value-of  select="$SecurityOfSitesRealEstate/sosre:ContractTerminationNote"/>,
              </xsl:when>
            </xsl:choose>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>