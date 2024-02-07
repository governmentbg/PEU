<xsl:stylesheet version="1.0" xmlns:APP="http://ereg.egov.bg/segment/R-3330"
                xmlns:HEADER="http://ereg.egov.bg/segment/R-3031"
                xmlns:EASH="http://ereg.egov.bg/segment/0009-000152"
                xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
                xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
                xmlns:ESR="http://ereg.egov.bg/segment/0009-000015"
                xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                xmlns:EBD="http://ereg.egov.bg/segment/0009-000013"
                xmlns:AFIVRD="http://ereg.egov.bg/segment/R-3331"
                xmlns:IVROD="http://ereg.egov.bg/segment/R-3332"
                xmlns:VDR="http://ereg.egov.bg/segment/R-3313"
                xmlns:RD="http://ereg.egov.bg/segment/R-3303"
                xmlns:PI="http://ereg.egov.bg/segment/0009-000006"
                
                xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                xmlns:xslExtension="urn:XSLExtension"
                
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				
                xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:include href="./KATBaseTemplates.xslt"/>

  <xsl:output omit-xml-declaration="yes" method="html"/>


  <xsl:template match="APP:ApplicationForInitialVehicleRegistration">
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
              <td colspan ="2">
                Телефон:&#160;<xsl:choose>
                  <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:Phone">
                    <xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:Phone"/>
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
                Рама (VIN) на ППС: <xsl:choose>
                  <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleIdentificationData/AFIVRD:IdentificationNumber">
                    <xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleIdentificationData/AFIVRD:IdentificationNumber"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Държава на внос: <xsl:choose>
                  <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleIdentificationData/AFIVRD:ImportCountryName">
                    <xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleIdentificationData/AFIVRD:ImportCountryName"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Знак за държава, издала типовото одобрение: <xsl:choose>
                  <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleIdentificationData/AFIVRD:ApprovalCountryCode">
                    <xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleIdentificationData/AFIVRD:ApprovalCountryCode"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>            
            <tr>
              <td colspan ="2">
                Цвят (основен) на ППС: <xsl:choose>
                <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleIdentificationData/AFIVRD:ColorName">
                  <xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleIdentificationData/AFIVRD:ColorName"/>
                </xsl:when>
              </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Допълнителна информация: <xsl:choose>
                  <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleIdentificationData/AFIVRD:AdditionalInfo">
                    <xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleIdentificationData/AFIVRD:AdditionalInfo"/>
                  </xsl:when>
                </xsl:choose>
              </td>
            </tr>
            <tr>
              <td colspan ="2">
                Собственост на следните физически/юридически лица:
              </td>
            </tr>

            <xsl:choose>
              <xsl:when test = "APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnersCollection/AFIVRD:InitialVehicleRegistrationOwnerData">
                <xsl:for-each select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnersCollection/AFIVRD:InitialVehicleRegistrationOwnerData">

                  <xsl:choose>
                    <xsl:when test="IVROD:PersonIdentifier">
                      <tr>
                        <td colspan="2">
                          ЕГН/ЛНЧ/ЛН:&#160;<xsl:if test="IVROD:PersonIdentifier/PI:EGN"><xsl:value-of  select="IVROD:PersonIdentifier/PI:EGN"/>&#160;</xsl:if><xsl:if test="IVROD:PersonIdentifier/PI:LNCh"><xsl:value-of  select="IVROD:PersonIdentifier/PI:LNCh"/>&#160;</xsl:if><xsl:if test="IVROD:IsVehicleRepresentative = 'true'">
                            лицето ще представи ППС в пункта „Пътна полиция“ за регистрация
                          </xsl:if><xsl:if test="(IVROD:IsVehicleRepresentative = 'true') and (IVROD:IsOwnerOfVehicleRegistrationCoupon = 'true')">&#160;и&#160;</xsl:if><xsl:if test="IVROD:IsOwnerOfVehicleRegistrationCoupon = 'true'">лицето ще бъде притежател на СРМПС</xsl:if>
                        </td>
                      </tr>
                    </xsl:when>
                    <xsl:when test="IVROD:Item">
                      <tr>
                        <td colspan="2">
                          ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="IVROD:Item"/>&#160;<xsl:if test="IVROD:IsVehicleRepresentative = 'true'">
                            лицето ще представи ППС в пункта „Пътна полиция“ за регистрация
                          </xsl:if><xsl:if test="(IVROD:IsVehicleRepresentative = 'true') and (IVROD:IsOwnerOfVehicleRegistrationCoupon = 'true')">&#160;и&#160;</xsl:if><xsl:if test="IVROD:IsOwnerOfVehicleRegistrationCoupon = 'true'">лицето ще бъде притежател на СРМПС</xsl:if>
                        </td>
                      </tr>
                    </xsl:when>
                  </xsl:choose>
                  
                </xsl:for-each>
              </xsl:when>
              
            </xsl:choose>

            <xsl:choose>
              <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnerOfRegistrationCoupon">
                <tr>
                  <td colspan ="2">
                    Притежател на свидетелството за регистрация на МПС:
                  </td>
                </tr>
                <xsl:choose>
                  <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnerOfRegistrationCoupon/IVROD:PersonIdentifier">
                    <tr>
                      <td colspan="2">
                        ЕГН/ЛНЧ/ЛН:&#160;<xsl:if test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnerOfRegistrationCoupon/IVROD:PersonIdentifier/PI:EGN"><xsl:value-of select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnerOfRegistrationCoupon/IVROD:PersonIdentifier/PI:EGN"/>&#160;</xsl:if><xsl:if test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnerOfRegistrationCoupon/IVROD:PersonIdentifier/PI:LNCh"><xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnerOfRegistrationCoupon/IVROD:PersonIdentifier/PI:LNCh"/>&#160;</xsl:if><xsl:if test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnerOfRegistrationCoupon/IVROD:IsVehicleRepresentative = 'true'">
                            лицето ще представи ППС в пункта „Пътна полиция“ за регистрация
                          </xsl:if>
                      </td>
                    </tr>
                  </xsl:when>
                  <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnerOfRegistrationCoupon/IVROD:Item">
                    <tr>
                      <td colspan="2">
                        ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnerOfRegistrationCoupon/IVROD:Item"/>&#160;<xsl:if test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:OwnerOfRegistrationCoupon/IVROD:IsVehicleRepresentative = 'true'">
                            лицето ще представи ППС в пункта „Пътна полиция“ за регистрация
                          </xsl:if>
                      </td>
                    </tr>
                  </xsl:when>
                </xsl:choose>
              </xsl:when>
            </xsl:choose>
            
            <xsl:choose>
              <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleUserData">
                <tr>
                  <td colspan ="2">
                    Ползвател на ППС:
                  </td>
                </tr>
                <xsl:choose>
                  <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleUserData/IVROD:PersonIdentifier">
                    <tr>
                      <td colspan="2">
                        ЕГН/ЛНЧ/ЛН:&#160;<xsl:if test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleUserData/IVROD:PersonIdentifier/PI:EGN"><xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleUserData/IVROD:PersonIdentifier/PI:EGN"/>&#160;</xsl:if><xsl:if test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleUserData/IVROD:PersonIdentifier/PI:LNCh"><xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleUserData/IVROD:PersonIdentifier/PI:LNCh"/>&#160;</xsl:if><xsl:if test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleUserData/IVROD:IsVehicleRepresentative = 'true'">
                            лицето ще представи ППС в пункта „Пътна полиция“ за регистрация
                          </xsl:if>
                      </td>
                    </tr>
                  </xsl:when>
                  <xsl:when test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleUserData/IVROD:Item">
                    <tr>
                      <td colspan="2">
                        ЕИК/БУЛСТАТ:&#160;<xsl:value-of  select="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleUserData/IVROD:Item"/>&#160;<xsl:if test="APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:VehicleUserData/IVROD:IsVehicleRepresentative = 'true'">
                            лицето ще представи ППС в пункта „Пътна полиция“ за регистрация
                          </xsl:if>
                      </td>
                    </tr>
                  </xsl:when>
                </xsl:choose>
              </xsl:when>
            </xsl:choose>
            
            <xsl:call-template name="AgreementToReceiveERefusal">
              <xsl:with-param name="AgreementToReceiveERefusal" select = "APP:ApplicationForInitialVehicleRegistrationData/AFIVRD:AgreementToReceiveERefusal" />
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