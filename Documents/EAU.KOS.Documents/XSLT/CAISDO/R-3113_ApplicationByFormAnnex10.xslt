<xsl:stylesheet version="1.0" xmlns:ABFA10="http://ereg.egov.bg/segment/R-3113"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
				        xmlns:ABFA10D="http://ereg.egov.bg/segment/R-3114"
				        xmlns:PI="http://ereg.egov.bg/segment/R-3015"
				        xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
				        xmlns:DBIF="http://ereg.egov.bg/segment/R-3041"
				        xmlns:IBDIP="http://ereg.egov.bg/segment/R-3033"
				        xmlns:OICIBID="http://ereg.egov.bg/value/R-3034"
				        xmlns:DMST="http://ereg.egov.bg/segment/R-3040"
				        xmlns:SARD="http://ereg.egov.bg/segment/0009-000141"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
				        xmlns:PD="http://ereg.egov.bg/segment/R-3037"
				        xmlns:E="http://ereg.egov.bg/segment/0009-000013"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./KOSBaseTemplates.xslt"/>
  
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="ABFA10:ApplicationByFormAnnex10">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th>
                &#160;
              </th>
              <th >
                <p align="right">
                  ПРИЛОЖЕНИЕ № 10
                </p>
              </th>
            </tr>
            <tr>
              <th>
                &#160;
              </th>
              <th >
                <p align="right">
                  <xsl:call-template name="IssuingPoliceDepartmentHeader">
                    <xsl:with-param name="IssuingPoliceDepartment" select = "ABFA10:IssuingPoliceDepartment" />
                  </xsl:call-template>
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
                <h2>ЗАЯВЛЕНИЕ</h2>
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
                &#160;
              </td>
            </tr>
            <xsl:call-template name="ApplicationElectronicServiceApplicantAndRepresentativeWithAddress">
              <xsl:with-param name="ElectronicServiceApplicant" select = "ABFA10:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant" />
              <xsl:with-param name="Phone" select = "ABFA10:ApplicationByFormAnnex10Data/ABFA10D:PersonalInformation/PI:MobilePhone" />
              <xsl:with-param name="PersonAddress" select = "ABFA10:ApplicationByFormAnnex10Data/ABFA10D:PersonalInformation/PI:PersonAddress" />
            </xsl:call-template>

            <tr>
              <td colspan ="2">
                <xsl:call-template name="IssuingPoliceDepartmentDirectTo">
                  <xsl:with-param name="IssuingPoliceDepartment" select = "ABFA10:IssuingPoliceDepartment" />
                </xsl:call-template>
              </td>
            </tr>
                          
            <tr>
              <td colspan ="2">
                <xsl:choose>
                  <xsl:when test="ABFA10:ApplicationByFormAnnex10Data/ABFA10D:PersonGrantedFromIssuingDocument">
                    &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Заявявам Ви, че желая да бъде издаден следния документ:
                  </xsl:when>
                  <xsl:otherwise>
                    &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Заявявам Ви, че желая да ми бъде издаден следния документ:
                  </xsl:otherwise>
                </xsl:choose>
              </td>
            </tr>
            
            <tr>
              <td colspan ="2">
                <xsl:value-of  select="ABFA10:ApplicationByFormAnnex10Data/ABFA10D:IssuingDocument"/>
              </td>

            </tr>

            <xsl:choose>
              <xsl:when test="ABFA10:ApplicationByFormAnnex10Data/ABFA10D:PersonGrantedFromIssuingDocument">
                <tr>
                  <td  colspan = "2">
                    на
                    &#160;<xsl:call-template name="PersonNames">
                      <xsl:with-param name="Names" select = "ABFA10:ApplicationByFormAnnex10Data/ABFA10D:PersonGrantedFromIssuingDocument/P:Names" />
                    </xsl:call-template>&#160;с<br/>
                    ЕГН/ЛНЧ/ЛН:&#160;<xsl:value-of  select="ABFA10:ApplicationByFormAnnex10Data/ABFA10D:PersonGrantedFromIssuingDocument/P:Identifier/."/>
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>

            <xsl:choose>
              <xsl:when test = "ABFA10:ApplicationByFormAnnex10Data/ABFA10D:SpecificDataForIssuingDocumentsForKOS">
                <tr>
                  <td colspan = "2">
                    Данни, специфични за издавания документ:&#160;<xsl:value-of  select="ABFA10:ApplicationByFormAnnex10Data/ABFA10D:SpecificDataForIssuingDocumentsForKOS"/>
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>

            <xsl:call-template name="AgreementToReceiveERefusal">
              <xsl:with-param name="AgreementToReceiveERefusal" select = "ABFA10:ApplicationByFormAnnex10Data/ABFA10D:AgreementToReceiveERefusal" />
            </xsl:call-template>
              
            <xsl:call-template name="Declarations">
              <xsl:with-param name="Declarations" select = "ABFA10:Declarations" />
              <xsl:with-param name="Declaration" select = "ABFA10:Declarations/ABFA10:Declaration" />
            </xsl:call-template>
            
             <xsl:call-template name="AttachedDocuments">
              <xsl:with-param name="AttachedDocuments" select = "ABFA10:AttachedDocuments" />
              <xsl:with-param name="AttachedDocument" select = "ABFA10:AttachedDocuments/ABFA10:AttachedDocument" />
            </xsl:call-template>
            
            <xsl:call-template name="ApplicationElectronicAdministrativeServiceFooterLite">
              <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "ABFA10:ElectronicAdministrativeServiceFooter" />
            </xsl:call-template>
            
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>