<xsl:stylesheet version="1.0" xmlns:NFF="http://ereg.egov.bg/segment/R-3052"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
				        xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
				        xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
				        xmlns:ID="http://ereg.egov.bg/segment/0009-000006"
				        xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
				        xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
				        xmlns:ABFA9D="http://ereg.egov.bg/segment/R-3042"
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
                xmlns:TSOW="http://ereg.egov.bg/segment/R-3054"
                xmlns:ARR="http://ereg.egov.bg/segment/R-3053"
                xmlns:xslExtension="urn:XSLExtension"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >
  <xsl:include href="./KOSBaseTemplates.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:template match="NFF:NotificationForFirearm">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body>
        <table align="center" cellpadding="5" width= "700px">
          <tbody>
            <tr>
              <th>
                &#160;
              </th>
              <th >
                <p align="left">
                  <xsl:call-template name="IssuingPoliceDepartmentHeader">
                    <xsl:with-param name="IssuingPoliceDepartment" select = "NFF:NotificationForFirearmData/ARR:IssuingPoliceDepartment" />
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
                <h2>УВЕДОМЛЕНИЕ</h2>
              </th>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;
              </td>
            </tr>
            <xsl:call-template name="ApplicationElectronicServiceApplicantAndRepresentativeWithAddress">
              <xsl:with-param name="ElectronicServiceApplicant" select = "NFF:ElectronicAdministrativeServiceHeader/EASH:ElectronicServiceApplicant" />
              <xsl:with-param name="Phone" select = "NFF:NotificationForFirearmData/ARR:ApplicantInformation/PI:MobilePhone" />
              <xsl:with-param name="PersonAddress" select = "NFF:NotificationForFirearmData/ARR:ApplicantInformation/PI:PersonAddress" />
            </xsl:call-template>
            <tr>
              <td colspan ="2">
                <xsl:call-template name="IssuingPoliceDepartmentDirectTo">
                  <xsl:with-param name="IssuingPoliceDepartment" select = "NFF:NotificationForFirearmData/ARR:IssuingPoliceDepartment" />
                </xsl:call-template>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Уведомявам Ви, че съм продал(a)/дарил(a)/заменил(a) огнестрелно оръжие със следните технически характеристики:<br/>
                <xsl:choose>
                  <xsl:when test = "NFF:NotificationForFirearmData/ARR:TechnicalSpecificationsOfWeapons">
                    <xsl:variable name="tswCount" select="count(NFF:NotificationForFirearmData/ARR:TechnicalSpecificationsOfWeapons/ARR:TechnicalSpecificationOfWeapon)"/>
                    <xsl:choose>
                      <xsl:when test="$tswCount > 1">
                        <table style="width:100%">
                          <tr>
                            <td style="padding: 5px">
                            </td>
                            <td style="padding: 5px">
                              марка
                            </td>
                            <td style="padding: 5px">
                              модел
                            </td>
                            <td style="padding: 5px">
                              калибър
                            </td>
                            <td style="padding: 5px">
                              № на оръжие
                            </td>
                            <td style="padding: 5px">
                              вид
                            </td>
                            <td style="padding: 5px">
                              предназначение
                            </td>
                          </tr>
                          <xsl:for-each select="NFF:NotificationForFirearmData/ARR:TechnicalSpecificationsOfWeapons/ARR:TechnicalSpecificationOfWeapon">
                            <tr>
                              <td style="padding: 5px">
                                <xsl:value-of select="position()"/>
                              </td>
                              <td style="padding: 5px">
                                <xsl:choose>
                                  <xsl:when test = "TSOW:Make">
                                    <xsl:value-of  select="TSOW:Make" />
                                  </xsl:when>
                                </xsl:choose>
                              </td>
                              <td style="padding: 5px">
                                <xsl:choose>
                                  <xsl:when test = "TSOW:Model">
                                    <xsl:value-of  select="TSOW:Model" />
                                  </xsl:when>
                                </xsl:choose>
                              </td>
                              <td style="padding: 5px">
                                <xsl:choose>
                                  <xsl:when test = "TSOW:Caliber">
                                    <xsl:value-of  select="TSOW:Caliber" />
                                  </xsl:when>
                                </xsl:choose>
                              </td>
                              <td style="padding: 5px">
                                <xsl:choose>
                                  <xsl:when test = "TSOW:WeaponNumber">
                                    <xsl:value-of  select="TSOW:WeaponNumber" />
                                  </xsl:when>
                                </xsl:choose>
                              </td>
                              <td style="padding: 5px">
                                <xsl:choose>
                                  <xsl:when test = "TSOW:WeaponTypeName">
                                    <xsl:value-of  select="TSOW:WeaponTypeName" />
                                  </xsl:when>
                                </xsl:choose>
                              </td>
                              <td style="padding: 5px">
                                <xsl:choose>
                                  <xsl:when test = "TSOW:WeaponPurposeName">
                                    <xsl:value-of  select="TSOW:WeaponPurposeName" />
                                  </xsl:when>
                                </xsl:choose>
                              </td>
                            </tr>
                          </xsl:for-each>
                        </table>
                      </xsl:when>
                      <xsl:otherwise>
                        <xsl:for-each select="NFF:NotificationForFirearmData/ARR:TechnicalSpecificationsOfWeapons/ARR:TechnicalSpecificationOfWeapon">
                          <xsl:value-of  select="TSOW:Make" />
                          <xsl:choose>
                            <xsl:when test = "TSOW:Model">
                              ,&#160;<xsl:value-of  select="TSOW:Model" />
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test = "TSOW:Caliber">
                              ,&#160;<xsl:value-of  select="TSOW:Caliber" />
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test = "TSOW:WeaponNumber">
                              ,&#160;<xsl:value-of  select="TSOW:WeaponNumber" />
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test = "TSOW:WeaponTypeName">
                              ,&#160;<xsl:value-of  select="TSOW:WeaponTypeName" />
                            </xsl:when>
                          </xsl:choose>
                          <xsl:choose>
                            <xsl:when test = "TSOW:WeaponPurposeName">
                              ,&#160;<xsl:value-of  select="TSOW:WeaponPurposeName" />
                            </xsl:when>
                          </xsl:choose>
                        </xsl:for-each>
                      </xsl:otherwise>
                    </xsl:choose>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan="2">
                Оръжието е придобито от лице с ЕИК:&#160; <xsl:value-of  select="NFF:NotificationForFirearmData/ARR:PurchaserUIC" />
              </td>
            </tr>
            <xsl:call-template name="AgreementToReceiveERefusal">
              <xsl:with-param name="AgreementToReceiveERefusal" select = "NFF:NotificationForFirearmData/ARR:AgreementToReceiveERefusal" />
            </xsl:call-template>
            <xsl:call-template name="Declarations">
              <xsl:with-param name="Declarations" select = "NFF:Declarations" />
              <xsl:with-param name="Declaration" select = "NFF:Declarations/NFF:Declaration" />
            </xsl:call-template>
            <xsl:call-template name="AttachedDocuments">
              <xsl:with-param name="AttachedDocuments" select = "NFF:AttachedDocuments" />
              <xsl:with-param name="AttachedDocument" select = "NFF:AttachedDocuments/NFF:AttachedDocument" />
            </xsl:call-template>
            <xsl:call-template name="ApplicationElectronicAdministrativeServiceFooter">
              <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "NFF:ElectronicAdministrativeServiceFooter" />
              <xsl:with-param name="SignatureXML" select = "$SignatureXML" />
            </xsl:call-template>
          </tbody>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>