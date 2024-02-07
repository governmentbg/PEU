<xsl:stylesheet version="1.0" xmlns:AFMORPAOIOV="http://ereg.egov.bg/segment/R-3322"
              xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
              xmlns:stbt="http://ereg.egov.bg/value/0008-000143"
              xmlns:sard="http://ereg.egov.bg/segment/0009-000141"
              xmlns:afmorpariovd="http://ereg.egov.bg/segment/R-3323"
              xmlns:dm="http://ereg.egov.bg/segment//R-3136"
              xmlns:ad="http://ereg.egov.bg/segment/0009-000139"
              xmlns:easf="http://ereg.egov.bg/segment/0009-000153"
              xmlns:pdc="http://ereg.egov.bg/segment/R-3037"
              xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
              xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
              xmlns:P="http://ereg.egov.bg/segment/0009-000008"
              xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
              xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
              xmlns:E="http://ereg.egov.bg/segment/0009-000013"
              xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
              xmlns:xslExtension="urn:XSLExtension"
                
              xmlns:xsl="http://www.w3.org/1999/XSL/Transform"

 xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./KATBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="AFMORPAOIOV:ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehicles">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th colspan ="2">
                <p align="right">
                  ДО<br/>
                  ДИРЕКТОРА НА<br/>
                  ГЛАВНА ДИРЕКЦИЯ „НАЦИОНАЛНА ПОЛИЦИЯ“
                </p>
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                &#160;
              </th>
            </tr>
            <tr>
              <th colspan ="2">
                <h2>
                  ЗАЯВЛЕНИЕ<br/>
                  за монтаж на регистрационни табели и/или идентификация на превозни средства на територията на областна дирекция на МВР, различна от областната дирекция по местоотчет на превозното средство
                </h2>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:call-template name="Applicant">
                  <xsl:with-param name="ElectronicServiceApplicant" select = "AFMORPAOIOV:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant" />
                  <xsl:with-param name="Phone" select = "AFMORPAOIOV:ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesData/afmorpariovd:Phone" />
                </xsl:call-template>
              </td>
            </tr>
            <tr>
              <td align="center" colspan ="2">
                <br/>
                <b>ГОСПОДИН ДИРЕКТОР,</b>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Предоставям следната информация, във връзка със заявената услуга:
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:value-of  select="AFMORPAOIOV:ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesData/afmorpariovd:ApplicationText/."/>
              </td>
            </tr>

            <xsl:call-template name="Declarations">
              <xsl:with-param name="Declarations" select = "AFMORPAOIOV:Declarations" />
              <xsl:with-param name="Declaration" select = "AFMORPAOIOV:Declarations/AFMORPAOIOV:Declaration" />
            </xsl:call-template>

            <xsl:call-template name="AttachedDocuments">
              <xsl:with-param name="AttachedDocuments" select = "AFMORPAOIOV:AttachedDocuments" />
              <xsl:with-param name="AttachedDocument" select = "AFMORPAOIOV:AttachedDocuments/AFMORPAOIOV:AttachedDocument" />
            </xsl:call-template>

            <xsl:call-template name="ApplicationElectronicAdministrativeServiceFooter">
              <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "AFMORPAOIOV:ElectronicAdministrativeServiceFooter" />
              <xsl:with-param name="SignatureXML" select = "$SignatureXML" />
            </xsl:call-template>

          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>


  <xsl:template name="Applicant">
    <xsl:param name="ElectronicServiceApplicant"/>
    <xsl:param name="Phone"/>
    От:&#160;
    <xsl:choose>
      <xsl:when test="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
        <xsl:value-of  select="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name/."/>
        ,&#160;ЕИК/БУЛСТАТ:&#160;
        <xsl:value-of  select="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier/."/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="PersonNames">
          <xsl:with-param name="Names" select = "$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names" />
        </xsl:call-template>
        ,&#160;ЕГН/ЛНЧ/ЛН:&#160;
        <xsl:value-of  select="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>
        <xsl:call-template name="IdentityDocument">
          <xsl:with-param name="IdentityDocument" select = "$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument" />
        </xsl:call-template>
      </xsl:otherwise>
    </xsl:choose>

    <br/><br/>Телефон: <xsl:value-of  select="$Phone/."/>

    <br/><br/>Адрес на електронна поща:<xsl:value-of  select="$ElectronicServiceApplicant/ESA:EmailAddress/."/>

    <xsl:if test="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1002' or
           $ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1003'" >
      <br/><br/>с пълномощник/представител:&#160;
      <xsl:call-template name="PersonNames">
        <xsl:with-param name="Names" select = "$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names" />
      </xsl:call-template>,&#160;ЕГН/ЛНЧ/ЛН:&#160;<xsl:value-of  select="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>,
      <xsl:call-template name="IdentityDocument">
        <xsl:with-param name="IdentityDocument" select = "$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument" />
      </xsl:call-template>
    </xsl:if>
  </xsl:template>


</xsl:stylesheet>



