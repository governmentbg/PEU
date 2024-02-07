<xsl:stylesheet version="1.0" xmlns:APP="http://ereg.egov.bg/segment/R-3150"
                xmlns:DATA="http://ereg.egov.bg/segment/R-3151"
                xmlns:PD="http://ereg.egov.bg/segment/R-3037"
				        xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
				        xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
                xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
                xmlns:E="http://ereg.egov.bg/segment/0009-000013"
                xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
                xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:NER="http://ereg.egov.bg/segment/R-3153"
				        xmlns:RER="http://ereg.egov.bg/segment/R-3154"
				        xmlns:NE="http://ereg.egov.bg/segment/R-3152"
                
                
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./CODBaseTemplates.xslt"/>
  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="APP:NotificationForConcludingOrTerminatingEmploymentContract">
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
                  ДО<br/>
                  НАЧАЛНИКА НА<br/>
                  <xsl:value-of select="APP:NotificationForConcludingOrTerminatingEmploymentContractData/DATA:IssuingPoliceDepartment/PD:PoliceDepartmentName" />
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
                  <xsl:when test="APP:NotificationForConcludingOrTerminatingEmploymentContractData/DATA:NotificationOfEmploymentContractType = 2001">
                    <p>за сключен трудов договор (по чл. 51, ал. 1 от ЗЧОД)</p>
                  </xsl:when>
                  <xsl:otherwise>
                    <p>за прекратен трудов договор (по чл. 51, ал. 2 от ЗЧОД)</p>
                  </xsl:otherwise>
                </xsl:choose>
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
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                От&#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name/."/><br/>
                ЕИК/БУЛСТАТ&#160;<xsl:value-of  select="APP:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier/."/><br/>
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
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                <xsl:choose>
                  <xsl:when test="APP:NotificationForConcludingOrTerminatingEmploymentContractData/DATA:NotificationOfEmploymentContractType = 2001">
                    <p>Уведомявам Ви за сключен трудов договор, съгласно чл. 51, ал. 1 от ЗЧОД на следното/ите лице/а:</p>
                  </xsl:when>
                  <xsl:otherwise>
                    <p>Уведомявам Ви за прекратен трудов договор, съгласно чл. 51, ал. 2 от ЗЧОД на следното/ите лице/а:</p>
                  </xsl:otherwise>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <xsl:choose>
              <xsl:when test="APP:NotificationForConcludingOrTerminatingEmploymentContractData/DATA:NotificationOfEmploymentContractType = 2001">
                <xsl:for-each select="APP:NotificationForConcludingOrTerminatingEmploymentContractData/DATA:NewEmployeeRequests/DATA:NewEmployeeRequest">
                  <tr>
                    <td colspan ="2">
                      <xsl:choose>
                        <xsl:when test="NER:Employee/NE:FullName">
                          <xsl:value-of  select="NER:Employee/NE:FullName"/>,
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="NER:Employee/NE:Identifier">
                          ЕГН/ЛН:&#160;<xsl:value-of  select="NER:Employee/NE:Identifier"/>,
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="NER:Employee/NE:Citizenship">
                          <xsl:choose>
                            <xsl:when test="NER:Employee/NE:Citizenship = 1">
                              &#160;Български гражданин,
                            </xsl:when>
                            <xsl:when test="NER:Employee/NE:Citizenship = 2">
                              &#160;Чужд гражданин,
                            </xsl:when>
                          </xsl:choose>
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="NER:ContractNumber">
                          № на трудов договор:&#160;<xsl:value-of  select="NER:ContractNumber"/>,
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="NER:ContractDate">
                          дата на сключване на трудовия договор:&#160;<xsl:value-of  select="ms:format-date(NER:ContractDate, 'dd.MM.yyyy')"/> г.,
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="NER:ContractType">
                          срок на сключения договор:&#160;<xsl:choose>
                            <xsl:when test="NER:ContractType = 2001">
                              безсрочен
                            </xsl:when>
                            <xsl:when test="NER:ContractType = 2002">
                              <xsl:choose>
                                <xsl:when test="NER:ContractPeriodInMonths">
                                  <xsl:value-of  select="NER:ContractPeriodInMonths"/>&#160;м.
                                </xsl:when>
                              </xsl:choose>
                            </xsl:when>
                          </xsl:choose>
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="NER:Employee/NE:SecurityObject">
                          , обект в който е назначен служителя&#160;<xsl:value-of  select="NER:Employee/NE:SecurityObject"/>,
                        </xsl:when>
                      </xsl:choose>
                    </td>
                  </tr>
                </xsl:for-each>
              </xsl:when>
              <xsl:otherwise>
                <xsl:for-each select="APP:NotificationForConcludingOrTerminatingEmploymentContractData/DATA:RemoveEmployeeRequests/DATA:RemoveEmployeeRequest">
                  <tr>
                    <td colspan ="2">
                      <xsl:choose>
                        <xsl:when test="RER:Employee/NE:FullName">
                          <xsl:value-of  select="RER:Employee/NE:FullName"/>,
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="RER:Employee/NE:Identifier">
                          ЕГН/ЛН:&#160;<xsl:value-of  select="RER:Employee/NE:Identifier"/>,
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="RER:Employee/NE:Citizenship">
                          <xsl:choose>
                            <xsl:when test="RER:Employee/NE:Citizenship = 1">
                              &#160;Български гражданин,
                            </xsl:when>
                            <xsl:when test="RER:Employee/NE:Citizenship = 2">
                              &#160;Чужд гражданин,
                            </xsl:when>
                          </xsl:choose>
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="RER:ContractTerminationNumber">
                          заповед за прекратяване на трудовия договор: №&#160;<xsl:value-of  select="RER:ContractTerminationNumber"/>,
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="RER:ContractTerminationDate">
                          от дата&#160;<xsl:value-of  select="ms:format-date(RER:ContractTerminationDate, 'dd.MM.yyyy')"/> г.,
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="RER:ContractTerminationEffectiveDate">
                          <br/>Дата, от която договорът се смята за прекратен:&#160;<xsl:value-of  select="ms:format-date(RER:ContractTerminationEffectiveDate, 'dd.MM.yyyy')"/> г.
                        </xsl:when>
                      </xsl:choose>
                      <xsl:choose>
                        <xsl:when test="RER:ContractTerminationNumber">
                          <br/>Забележка:&#160;<xsl:value-of  select="RER:ContractTerminationNote"/>,
                        </xsl:when>
                      </xsl:choose>
                    </td>
                  </tr>
                </xsl:for-each>
              </xsl:otherwise>
            </xsl:choose>
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
                &#160;
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <tr>
              <td width="50%">
                Дата:&#160;<xsl:value-of  select="ms:format-date(APP:ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime , 'dd.MM.yyyy')"/>г.
              </td>
              <td width="50%">
              </td>
            </tr>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>