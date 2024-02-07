<xsl:stylesheet version="1.0" xmlns:APP="http://ereg.egov.bg/segment/R-3318"
                xmlns:HEADER="http://ereg.egov.bg/segment/R-3031"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
                xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
                xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
                xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                xmlns:VDR="http://ereg.egov.bg/segment/R-3313"
                xmlns:RD="http://ereg.egov.bg/segment/R-3303"
                xmlns:PI="http://ereg.egov.bg/segment/0009-000006"
                
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xslExtension="urn:XSLExtension"
                
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
                xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./KATBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:output omit-xml-declaration="yes" method="html"/>


  <xsl:template match="APP:ApplicationForTemporaryTakingOffRoadOfVehicle">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <thead>
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
            <tr>
              <th colspan ="2">
                &#160;
              </th>
            </tr>
          </thead>

          <tbody>
            <tr>
              <td colspan ="2">
                От&#160;<xsl:call-template name="PersonNames">
                  <xsl:with-param name="Names" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names" />
                </xsl:call-template>,&#160;ЕГН/ЛНЧ/ЛН&#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>,<xsl:call-template name="IdentityDocument">
                  <xsl:with-param name="IdentityDocument" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument" />
                </xsl:call-template><xsl:if test="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1002'">, в качеството на пълномощник на друго физическо или юридическо лице.</xsl:if><xsl:if test="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType = 'R-1003'">, в качеството на законен представител на юридическо лице.</xsl:if>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:call-template name="ApplicationElectronicServiceApplicantEmail">
                  <xsl:with-param name="ElectronicServiceApplicant" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant" />
                </xsl:call-template>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Телефон:&#160;<xsl:choose>
                  <xsl:when test="APP:VehicleDataRequest/VDR:Phone">
                    <xsl:value-of  select="APP:VehicleDataRequest/VDR:Phone"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Заявявам услугата за пътно превозно средство (ППС) със следните данни:
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Регистрационен номер: <xsl:choose>
                  <xsl:when test="APP:VehicleDataRequest/VDR:RegistrationData/RD:RegistrationNumber">
                    <xsl:value-of  select="APP:VehicleDataRequest/VDR:RegistrationData/RD:RegistrationNumber"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test="APP:VehicleDataRequest/VDR:RegistrationData/RD:RegistrationCertificateNumber">
                <tr>
                  <td colspan ="2">
                    <xsl:if test="APP:VehicleDataRequest/VDR:RegistrationData/RD:RegistrationCertificateType = '1000'">Свидетелство за регистрация на ППС:</xsl:if><xsl:if test="APP:VehicleDataRequest/VDR:RegistrationData/RD:RegistrationCertificateType = '1001'">Регистрационен талон:</xsl:if>&#160;<xsl:value-of  select="APP:VehicleDataRequest/VDR:RegistrationData/RD:RegistrationCertificateNumber"/>
                  </td>
                </tr>
              </xsl:when>
            </xsl:choose>
            <tr>
              <td colspan ="2">
                Собственост на следните физически/юридически лица:
              </td>
            </tr>

            <xsl:choose>
              <xsl:when test = "APP:VehicleDataRequest/VDR:OwnersCollection">
                <xsl:for-each select="APP:VehicleDataRequest/VDR:OwnersCollection/VDR:Owners">

                  <xsl:choose>
                    <xsl:when test="VDR:PersonIdentifier">
                      <tr>
                        <td colspan="2">
                          ЕГН/ЛНЧ/ЛН:&#160;<xsl:if test="VDR:PersonIdentifier/PI:EGN">
                          <xsl:value-of  select="VDR:PersonIdentifier/PI:EGN"/>
                        </xsl:if><xsl:if test="VDR:PersonIdentifier/PI:LNCh">
                          <xsl:value-of  select="VDR:PersonIdentifier/PI:LNCh"/>
                        </xsl:if>
                        </td>
                      </tr>
                    </xsl:when>
                    <xsl:when test="VDR:Item">
                      <tr>
                        <td colspan="2">
                          ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="VDR:Item"/>
                        </td>
                      </tr>
                    </xsl:when>
                  </xsl:choose>
                </xsl:for-each>
              </xsl:when>
            </xsl:choose>
            
            <xsl:call-template name="AgreementToReceiveERefusal">
              <xsl:with-param name="AgreementToReceiveERefusal" select = "APP:VehicleDataRequest/VDR:AgreementToReceiveERefusal" />
            </xsl:call-template>

            <xsl:call-template name="Declarations">
              <xsl:with-param name="Declarations" select = "APP:Declarations" />
              <xsl:with-param name="Declaration" select = "APP:Declarations/APP:Declaration" />
            </xsl:call-template>

            <xsl:call-template name="AttachedDocuments">
              <xsl:with-param name="AttachedDocuments" select = "APP:AttachedDocuments" />
              <xsl:with-param name="AttachedDocument" select = "APP:AttachedDocuments/APP:AttachedDocument" />
            </xsl:call-template>
            
            <xsl:call-template name="ApplicationElectronicAdministrativeServiceFooter">
              <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "APP:ElectronicAdministrativeServiceFooter" />
              <xsl:with-param name="SignatureXML" select = "$SignatureXML" />
            </xsl:call-template>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>