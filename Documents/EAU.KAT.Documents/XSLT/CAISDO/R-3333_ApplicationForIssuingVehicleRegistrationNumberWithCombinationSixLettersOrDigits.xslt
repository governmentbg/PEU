<xsl:stylesheet version="1.0" xmlns:APP="http://ereg.egov.bg/segment/R-3333"
                xmlns:HEADER="http://ereg.egov.bg/segment/R-3031"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
                xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
                xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
                xmlns:ESR="http://ereg.egov.bg/segment/0009-000015"
                xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                xmlns:EBD="http://ereg.egov.bg/segment/0009-000013"
                xmlns:DATA="http://ereg.egov.bg/segment/R-3334"
                xmlns:PD="http://ereg.egov.bg/segment/R-3037"
                
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xslExtension="urn:XSLExtension"
                
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
                xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./KATBaseTemplates.xslt"/>
  
  <xsl:output omit-xml-declaration="yes" method="html"/>


  <xsl:template match="APP:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
            <tr>
              <th style="width: 65%">&#160;</th>
              <th>
                <div style="text-align: left;">
                  <h3>ДО</h3>
                  <h3>НАЧАЛНИКА НА</h3>
                  <h3>
                    "ПЪТНА ПОЛИЦИЯ"&#160;-&#160;<xsl:value-of select="APP:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData/DATA:IssuingPoliceDepartment/PD:PoliceDepartmentName" />
                  </h3>
                </div>
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
                  <xsl:value-of select="APP:ElectronicAdministrativeServiceHeader/EASH:DocumentTypeName" />
                </h2>
              </th>
            </tr>
          </thead>

          <tbody>
            <tr>
              <td colspan ="2">
                От:&#160;<xsl:choose>
                  <xsl:when test="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/ESR:Person">
                    <xsl:call-template name="PersonNames">
                      <xsl:with-param name="Names" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/ESR:Person/P:Names" />
                    </xsl:call-template>, ЕГН/ЛНЧ/ЛН:&#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/ESR:Person/P:Identifier/."/>,&#160;<xsl:call-template name="IdentityDocument">
                    <xsl:with-param name="IdentityDocument" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/ESR:Person/P:IdentityDocument" />
                  </xsl:call-template>
                  </xsl:when>
                  <xsl:otherwise>
                    <xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/ESR:Entity/EBD:Name/."/>, ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/ESR:Entity/EBD:Identifier/."/>
                  </xsl:otherwise>
                </xsl:choose>
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
                <tr>
                  <td colspan ="2">
                    с пълномощник/представител:&#160;<xsl:call-template name="PersonNames">
                    <xsl:with-param name="Names" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names" />
                  </xsl:call-template>, ЕГН/ЛНЧ/ЛН:&#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>,&#160;<xsl:call-template name="IdentityDocument">
                    <xsl:with-param name="IdentityDocument" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument" />
                  </xsl:call-template>
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>           
            <tr>
              <td colspan ="2">
                <xsl:call-template name="ApplicationElectronicServiceApplicantEmail">
                  <xsl:with-param name="ElectronicServiceApplicant" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant" />
                </xsl:call-template>
              </td>
            </tr>
            <tr>
              <th colspan ="2">
                &#160;
              </th>
            </tr>
            <tr>
              <td colspan ="2">
                <div style="text-align: center;">ГОСПОДИН НАЧАЛНИК,</div>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Моля, да получа национална&#160;<xsl:value-of select="APP:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData/DATA:AISKATVehicleTypeName"/>&#160;с регистрационен номер&#160;<xsl:if test="APP:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData/DATA:PlatesContentType = '1'">
                  с 4 цифри
                </xsl:if><xsl:if test="APP:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData/DATA:PlatesContentType = '2'">с комбинация от 6 букви и/или цифри</xsl:if>&#160;<xsl:value-of  select="APP:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData/DATA:WishedRegistrationNumber"/>
              </td>
            </tr>
            
            <xsl:choose>
              <xsl:when test="APP:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData/DATA:RectangularPlatesCount">
                <tr>
                  <td colspan ="2">
                    Брой табели – правоъгълни (Тип1): <xsl:value-of  select="APP:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData/DATA:RectangularPlatesCount"/><br/>
                    Брой табели – квадратни (Тип2): <xsl:value-of  select="APP:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData/DATA:SquarePlatesCount"/>
                  </td>
                </tr>              
              </xsl:when>
            </xsl:choose>            

            <xsl:call-template name="AgreementToReceiveERefusal">
              <xsl:with-param name="AgreementToReceiveERefusal" select = "APP:ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData/DATA:AgreementToReceiveERefusal" />
            </xsl:call-template>

            <xsl:call-template name="Declarations">
              <xsl:with-param name="Declarations" select = "APP:Declarations" />
              <xsl:with-param name="Declaration" select = "APP:Declarations/APP:Declaration" />
            </xsl:call-template>

            <xsl:call-template name="AttachedDocuments">
              <xsl:with-param name="AttachedDocuments" select = "APP:AttachedDocuments" />
              <xsl:with-param name="AttachedDocument" select = "APP:AttachedDocuments/APP:AttachedDocument" />
            </xsl:call-template>
            
            <xsl:call-template name="ApplicationElectronicAdministrativeServiceFooterLite">
              <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "APP:ElectronicAdministrativeServiceFooter" />
            </xsl:call-template>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>