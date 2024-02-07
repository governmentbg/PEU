<xsl:stylesheet version="1.0" xmlns:APP="http://ereg.egov.bg/segment/R-3160"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
                 xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
                 xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
                    xmlns:E="http://ereg.egov.bg/segment/0009-000013"
                xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
                xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
            xmlns:stbt="http://ereg.egov.bg/value/0008-000143"
            xmlns:sard="http://ereg.egov.bg/segment/0009-000141"
            xmlns:DATA="http://ereg.egov.bg/segment/R-3161"
            xmlns:SODATA="http://ereg.egov.bg/segment/R-3162"
            xmlns:dm="http://ereg.egov.bg/segment//R-3136"
            xmlns:ad="http://ereg.egov.bg/segment/0009-000139"
            xmlns:easf="http://ereg.egov.bg/segment/0009-000153"
                xmlns:pdc="http://ereg.egov.bg/segment/R-3037"
                 xmlns:nt="http://ereg.egov.bg/segment/R-2214"
                 xmlns:scd="http://ereg.egov.bg/segment/R-3163"
                 xmlns:so="http://ereg.egov.bg/segment/R-3176"
               
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./CODBaseTemplates.xslt"/>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="APP:NotificationForTakingOrRemovingFromSecurity">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <tr>
            <th>
              &#160;
            </th>
            <th >
              <p align="right">
                ДО<br/>
                НАЧАЛНИКА НА<br/>
                <xsl:value-of select="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:IssuingPoliceDepartment/pdc:PoliceDepartmentName" />
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
              <h2>УВЕДОМЛЕНИЕ</h2>
              <xsl:choose>
                <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:NotificationType/. = 1">
                  <p>за сключен договор за охрана (съгласно чл. 52, ал. 5 от ЗЧОД)</p>
                </xsl:when>
                <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:NotificationType/. = 2">
                  <p>за сключен договор за охрана (съгласно чл. 52, ал. 4 от ЗЧОД)</p>
                </xsl:when>
                <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:NotificationType/. = 4">
                  <p>за прекратен договор за охрана (съгласно чл. 52, ал. 4 от ЗЧОД)</p>
                </xsl:when>
                <xsl:otherwise>
                  <p>за прекратен договор за охрана (съгласно чл. 52, ал. 5 от ЗЧОД)</p>
                </xsl:otherwise>
              </xsl:choose>
            </th>
          </tr>
          <tr>
            <td colspan ="2">
              &#160;
            </td>
          </tr>
          <tr>
            <td colspan ="2">
              Представител:&#160;<xsl:call-template name="PersonNames">
                <xsl:with-param name="Names" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names" />
              </xsl:call-template>,&#160;ЕГН/ЛНЧ/ЛН&#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/><br/>
              Документ за самоличност:<xsl:call-template name="IdentityDocument">
                <xsl:with-param name="IdentityDocument" select = "APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument" />
              </xsl:call-template>
            </td>
          </tr>
          <tr>
            <td colspan ="2">
              Уведомяваме Ви, че търговецът
              <xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name/."/>,
              с ЕИК&#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier/."/>
              &#160;
              <xsl:choose>
                <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:NotificationType/. = 1">
                  e сключил договор за охрана.
                  <p>
                    Номер на договора&#160;
                    <xsl:value-of select="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractNumber" />
                    &#160;от дата&#160;
                    <xsl:call-template name="Date">
                      <xsl:with-param name="Date" select = "APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractDate" />
                    </xsl:call-template>
                  </p>
                </xsl:when>
                <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:NotificationType/. = 2">
                пое за самоохрана обект по чл. 5, ал. 1, т. 4 от ЗЧОД - самоохрана на имущество на лица по чл. 2, ал. 3:
                  <p>
                    Номер на заповед&#160;
                    <xsl:value-of select="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractNumber" />
                    &#160;от дата&#160;
                    <xsl:call-template name="Date">
                      <xsl:with-param name="Date" select = "APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractDate" />
                    </xsl:call-template>
                  </p>
                </xsl:when>
                <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:NotificationType/. = 4">
                  е снел охраната на обект по чл. 5, ал. 1, т. 4 от ЗЧОД - самоохрана на имущество на лица по чл. 2, ал. 3
                  <p>
                    <xsl:choose>
                      <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractType">
                        Вид на документа за прекратяване&#160;
                        <xsl:value-of select="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractType" /><br/>
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:DocumentNumber">
                        Номер на документа&#160;
                        <xsl:value-of select="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:DocumentNumber" />
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractDate">
                        &#160;от дата&#160;
                        <xsl:call-template name="Date">
                          <xsl:with-param name="Date" select = "APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractDate" />
                        </xsl:call-template>
                      </xsl:when>
                    </xsl:choose>
                  </p>
                </xsl:when>
                <xsl:otherwise>
                  e прекратил договор за охрана
                  <p>
                    <xsl:choose>
                      <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractType">
                        Вид на документа за прекратяване&#160;
                        <xsl:value-of select="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractType" /><br/>
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:DocumentNumber">
                        Номер на документа&#160;
                        <xsl:value-of select="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:DocumentNumber" />
                      </xsl:when>
                    </xsl:choose>
                    <xsl:choose>
                      <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractDate">
                        &#160;от дата&#160;
                        <xsl:call-template name="Date">
                          <xsl:with-param name="Date" select = "APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractDate" />
                        </xsl:call-template>
                      </xsl:when>
                    </xsl:choose>
                  </p>
                  <xsl:if test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:SecurityContractData/scd:ContractIsExpired = 'true'">
                    ПОРАДИ ИЗТИЧАНЕ НА ДОГОВОРА
                    <br/>
                  </xsl:if>
                </xsl:otherwise>
              </xsl:choose>
            </td>
          </tr>
          <tr>
            <td colspan ="2">
              <xsl:call-template name="ContractAssignor">
                <xsl:with-param name="ContractAssignor" select = "APP:NotificationForTakingOrRemovingFromSecurityData/DATA:ContractAssignor" />
              </xsl:call-template>
            </td>
          </tr>
          <tr>
            <td colspan ="2">

              <xsl:choose>
                <xsl:when test="APP:NotificationForTakingOrRemovingFromSecurityData/DATA:NotificationType/. = 1 or APP:NotificationForTakingOrRemovingFromSecurityData/DATA:NotificationType/. = 2">
                  <xsl:for-each select="APP:SecurityObjectsData/SODATA:SecurityObjects/SODATA:SecurityObject">
                    <xsl:call-template name="SecurityObject">
                      <xsl:with-param name="ForTermination" select = "0" />
                    </xsl:call-template>
                    <hr></hr>
                  </xsl:for-each>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:for-each select="APP:SecurityObjectsData/SODATA:SecurityObjects/SODATA:SecurityObject">
                    <xsl:call-template name="SecurityObject">
                      <xsl:with-param name="ForTermination" select = "1" />
                    </xsl:call-template>
                    <hr></hr>
                  </xsl:for-each>
                </xsl:otherwise>
              </xsl:choose>            
            </td>
          </tr>
          <!--<xsl:call-template name="Declarations">
              <xsl:with-param name="Declarations" select = "APP:Declarations" />
              <xsl:with-param name="Declaration" select = "APP:Declarations/APP:Declaration" />
            </xsl:call-template>

            <xsl:call-template name="AttachedDocuments">
              <xsl:with-param name="AttachedDocuments" select = "APP:AttachedDocuments" />
              <xsl:with-param name="AttachedDocument" select = "APP:AttachedDocuments/APP:AttachedDocument" />
            </xsl:call-template>-->

          <xsl:call-template name="ApplicationElectronicAdministrativeServiceFooterLite">
            <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "APP:ElectronicAdministrativeServiceFooter" />
          </xsl:call-template>

        </table>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
