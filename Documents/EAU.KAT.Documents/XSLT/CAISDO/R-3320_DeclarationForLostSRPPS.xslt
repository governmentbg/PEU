<xsl:stylesheet version="1.0" xmlns:APP="http://ereg.egov.bg/segment/R-3320"
                xmlns:HEADER="http://ereg.egov.bg/segment/R-3031"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
                xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
                xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
                xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                xmlns:DM="http://ereg.egov.bg/segment//R-3136"
                
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xslExtension="urn:XSLExtension"
                
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
                xmlns:ms="urn:schemas-microsoft-com:xslt" 
                xsi:type="xsl:transform" >

  <xsl:include href="./KATBaseTemplates.xslt"/>

  <xsl:output omit-xml-declaration="yes" method="html"/>


  <xsl:template match="APP:DeclarationForLostSRPPS">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th colspan ="2">
                <h2>
                  <xsl:value-of select="APP:ElectronicAdministrativeServiceHeader/EASH:DocumentTypeName" />
                </h2>
              </th>
            </tr>
          </thead>

          <tbody>
            <tr>
              <td colspan ="2">
                От&#160;<xsl:call-template name="PersonNames">
                <xsl:with-param name="Names" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names" />
                </xsl:call-template>&#160;-&#160;<xsl:if test="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1001'">собственик</xsl:if><xsl:if test="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">пълномощник</xsl:if>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                с ЕГН/ЛНЧ/ЛН&#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>,
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:call-template name="IdentityDocument">
                  <xsl:with-param name="IdentityDocument" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument" />
                </xsl:call-template>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:choose>
                  <xsl:when test="APP:Data/APP:Declaration">
                    <xsl:value-of select="APP:Data/APP:Declaration" disable-output-escaping="yes"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            
            <xsl:call-template name="ApplicationElectronicAdministrativeServiceFooterLite">
              <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "APP:ElectronicAdministrativeServiceFooter" />
            </xsl:call-template>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>