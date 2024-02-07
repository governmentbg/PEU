<xsl:stylesheet version="1.0" xmlns:APP="http://ereg.egov.bg/segment/R-3340"
                
          xmlns:DURI="http://ereg.egov.bg/segment/0009-000001"
          xmlns:VRD="http://ereg.egov.bg/segment/R-3303"
          xmlns:VC="http://ereg.egov.bg/segment/R-3307"
          xmlns:PD="http://ereg.egov.bg/segment/R-3037"
          xmlns:PDATA="http://ereg.egov.bg/segment/R-3300"
          xmlns:PBD="http://ereg.egov.bg/segment/0009-000008"
          xmlns:N="http://ereg.egov.bg/segment/0009-000005"
          xmlns:PI="http://ereg.egov.bg/segment/0009-000006"
          xmlns:EDATA="http://ereg.egov.bg/segment/R-3302"
          xmlns:ID="http://ereg.egov.bg/segment/0009-000099"
                
          xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
          xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
          xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:include href="./KATBaseTemplatesNewDesign.xslt"/>

  <xsl:template match="APP:ReportForVehicle">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body cz-shortcut-listen="true">
        <div class="print-document flex-container">
          <div class="document-section document-header">
            <h1 class="text-center document-title">
              <xsl:value-of select="APP:DocumentTypeName" /><br/>
              №: <xsl:value-of select="APP:DocumentURI/DURI:RegisterIndex" />-<xsl:value-of select="APP:DocumentURI/DURI:SequenceNumber" />-<xsl:value-of  select="ms:format-date(APP:DocumentURI/DURI:ReceiptOrSigningDate , 'dd.MM.yyyy')"/>
            </h1>
          </div>
          <div class="document-section">
            <h2 class="section-title section-line">Справки по данни за ППС</h2>
            <xsl:choose>
              <xsl:when test="APP:RPSSVehicleData">
                <div class="document-section">
                  <h3 class="section-title">Справка в АИС КАТ „Регистрация на ППС и собствениците им“</h3>
                  <div class="row-table">
                    <div class="flex-row">
                      <div class="flex-col">
                        <span class="label">Регистрационен номер:</span>&#160;<xsl:value-of select="APP:RPSSVehicleData/APP:VehicleRegistrationData/VRD:RegistrationNumber" />
                      </div>
                    </div>
                    <div class="flex-row">
                      <div class="flex-col-6">
                        <span class="label">Марка и модел:</span>&#160;<xsl:value-of select="APP:RPSSVehicleData/APP:VehicleRegistrationData/VRD:MakeAndModel" />
                      </div>
                      <div class="flex-col-6">
                        <span class="label">Рама (VIN) на ППС:</span>&#160;<xsl:value-of select="APP:RPSSVehicleData/APP:VehicleRegistrationData/VRD:IdentificationNumber" />
                      </div>
                    </div>
                    <div class="flex-row">
                      <div class="flex-col-6">
                        <span class="label">Категория на ППС:</span>&#160;<xsl:value-of select="APP:RPSSVehicleData/APP:VehicleRegistrationData/VRD:VehicleCategory/VC:Name" />
                      </div>
                      <div class="flex-col-6">
                        <span class="label">Дата на следващ технически преглед:</span>&#160;<xsl:value-of  select="ms:format-date(APP:RPSSVehicleData/APP:VehicleRegistrationData/VRD:NextVehicleInspection, 'dd.MM.yyyy г.')"/>
                      </div>
                    </div>
                    <div class="flex-row">
                      <div class="flex-col">
                        <span class="label">Структура на МВР, регистрирала ППС:</span>&#160;<xsl:value-of select="APP:RPSSVehicleData/APP:VehicleRegistrationData/VRD:PoliceDepartment/PD:PoliceDepartmentName" />
                      </div>
                    </div>
                    <div class="flex-row">
                      <div class="flex-col">
                        <xsl:call-template name="Statuses">
                          <xsl:with-param name="Statuses" select="APP:RPSSVehicleData/APP:VehicleRegistrationData/APP:Statuses"/>
                          <xsl:with-param name="Status" select="APP:RPSSVehicleData/APP:VehicleRegistrationData/APP:Statuses/APP:Status"/>
                        </xsl:call-template>
                      </div>
                    </div>
                  </div>
                  <div class="document-section">
                    <h4 class="section-title">Данни за собственици</h4>
                    <ol class="list-field list-counter">
                      <xsl:for-each select="APP:RPSSVehicleData/APP:Owners">
                        <xsl:choose>
                          <xsl:when test="APP:PersonData">
                            <li>
                              <div class="row-table">
                                <div class="flex-row">
                                  <div class="flex-col">
                                    <xsl:call-template name="PersonBasicDataNames">
                                      <xsl:with-param name="Names" select="APP:PersonData/PDATA:PersonBasicData/PBD:Names"/>
                                    </xsl:call-template>
                                  </div>
                                </div>
                                <div class="flex-row">
                                  <div class="flex-col">
                                    <xsl:call-template name="PersonBasicDataIdentifier">
                                      <xsl:with-param name="EGN" select="APP:PersonData/PDATA:PersonBasicData/PBD:Identifier/PI:EGN"/>
                                      <xsl:with-param name="LNCH" select="APP:PersonData/PDATA:PersonBasicData/PBD:Identifier/PI:LNCh"/>
                                    </xsl:call-template>
                                  </div>
                                </div>
                                <div class="flex-row">
                                  <div class="flex-col">
                                    <xsl:call-template name="GRAOAddress">
                                      <xsl:with-param name="GRAOAddress" select = "APP:PersonData/PDATA:PermanentAddress" />
                                      <xsl:with-param name="AddressLabel" select = '"Адрес"' />
                                    </xsl:call-template>
                                  </div>
                                </div>
                              </div>
                            </li>
                          </xsl:when>
                          <xsl:otherwise>
                            <li>
                              <div class="row-table">
                                <div class="flex-row">
                                  <div class="flex-col">
                                    <span class="label">Наименование:</span>&#160;<xsl:value-of  select="APP:EntityData/EDATA:FullName"/>
                                  </div>
                                </div>
                                <div class="flex-row">
                                  <div class="flex-col">
                                    <span class="label">ЕИК/БУЛСТАТ:</span>&#160;<xsl:value-of  select="APP:EntityData/EDATA:Identifier"/>
                                  </div>
                                </div>
                                <div class="flex-row">
                                  <div class="flex-col">
                                    <xsl:call-template name="GRAOAddress">
                                      <xsl:with-param name="GRAOAddress" select = "APP:EntityData/EDATA:EntityManagmentAddress" />
                                      <xsl:with-param name="AddressLabel" select = '"Адрес"' />
                                    </xsl:call-template>
                                  </div>
                                </div>
                              </div>
                            </li>
                          </xsl:otherwise>
                        </xsl:choose>
                      </xsl:for-each>
                    </ol>
                    <xsl:call-template name='Statuses'>
                      <xsl:with-param name='Statuses' select='APP:RPSSVehicleData/APP:Statuses' />
                      <xsl:with-param name='Status' select='APP:RPSSVehicleData/APP:Statuses/APP:Status' />
                    </xsl:call-template>
                  </div>
                </div>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test='APP:EUCARISData'>
                <div class="document-section">
                  <div class="row-table">
                    <div class="flex-row">
                      <div class="flex-col-4">
                        <span class="label">Цвят (основен) на ППС:</span>&#160;<xsl:value-of  select="APP:EUCARISData/APP:ColorName"/>
                      </div>
                      <div class="flex-col-8">
                        <span class="label">Допълнителна информация:</span>&#160;<xsl:value-of  select="APP:EUCARISData/APP:AdditionalInfo"/>
                      </div>
                    </div>
                  </div>
                  <xsl:choose>
                    <xsl:when test="APP:EUCARISData/APP:CanUseCertificateForRegistration = 'true'">
                      <p>
                        <span class="label">Има данни за eCoC и тези данни могат да се използват за регистрация на ППС в АИС „КАТ-Регистрация“.</span>
                      </p>
                    </xsl:when>
                  </xsl:choose>
                  <xsl:choose>
                    <xsl:when test="APP:EUCARISData/APP:Statuses">
                      <xsl:call-template name="Statuses">
                        <xsl:with-param name="Statuses" select="APP:EUCARISData/APP:Statuses"/>
                        <xsl:with-param name="Status" select="APP:EUCARISData/APP:Statuses"/>
                      </xsl:call-template>
                    </xsl:when>
                  </xsl:choose>
                </div>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="APP:GuaranteeFund">
                <div class="document-section">
                  <xsl:call-template name="GuaranteeFund">
                    <xsl:with-param name="PolicyValidTo" select="APP:GuaranteeFund/APP:PolicyValidTo"/>
                    <xsl:with-param name="Statuses" select="APP:GuaranteeFund/APP:Status"/>
                  </xsl:call-template>
                </div>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test="APP:PeriodicTechnicalCheck">
                <div class="document-section">
                  <xsl:call-template name="PeriodicTechnicalCheck">
                    <xsl:with-param name="NextInspectionDate" select="APP:PeriodicTechnicalCheck/APP:NextInspectionDate"/>
                    <xsl:with-param name="Statuses" select="APP:PeriodicTechnicalCheck/APP:Status"/>
                  </xsl:call-template>
                </div>
              </xsl:when>
            </xsl:choose>
          </div>
          <div class="document-section">
            <h2 class="section-title section-line">Справки по данни за собственици</h2>
            <xsl:choose>
              <xsl:when test='APP:Owners/APP:PersonData'>
                <div class="document-section">
                  <h3 class="section-title">Справка за физически лица в НАИФ НРБЛД</h3>
                  <ol class="list-field list-counter">
                    <xsl:for-each select="APP:Owners/APP:PersonData">
                      <li>
                        <div class="row-table">
                          <div class="flex-row">
                            <div class="flex-col-8">
                              <xsl:call-template name="PersonBasicDataNames">
                                <xsl:with-param name="Names" select="PDATA:PersonBasicData/PBD:Names"/>
                              </xsl:call-template>
                            </div>
                            <div class="flex-col-4">
                              <xsl:call-template name="PersonBasicDataIdentifier">
                                <xsl:with-param name="EGN" select="PDATA:PersonBasicData/PBD:Identifier/PI:EGN"/>
                                <xsl:with-param name="LNCH" select="PDATA:PersonBasicData/PBD:Identifier/PI:LNCh"/>
                              </xsl:call-template>
                            </div>
                          </div>
                          <div class="flex-row">
                            <div class="flex-col">
                              <xsl:call-template name="IdentityDocumentType">
                                <xsl:with-param name="IdentityDocumentType" select="PDATA:PersonBasicData/PBD:IdentityDocument/ID:IdentityDocumentType"/>
                              </xsl:call-template>
                            </div>
                          </div>
                          <div class="flex-row">
                            <div class="flex-col-4">
                              <xsl:call-template name="IdentityNumber">
                                <xsl:with-param name="IdentityNumber" select="PDATA:PersonBasicData/PBD:IdentityDocument/ID:IdentityNumber"/>
                              </xsl:call-template>
                            </div>
                            <div class="flex-col-4">
                              <xsl:call-template name="IdentitityIssueDate">
                                <xsl:with-param name="IdentitityIssueDate" select="PDATA:PersonBasicData/PBD:IdentityDocument/ID:IdentitityIssueDate"/>
                              </xsl:call-template>
                            </div>
                            <div class="flex-col-4">
                              <xsl:call-template name="IdentityIssuer">
                                <xsl:with-param name="IdentityIssuer" select="PDATA:PersonBasicData/PBD:IdentityDocument/ID:IdentityIssuer"/>
                              </xsl:call-template>
                            </div>
                          </div>
                          <div class="flex-row">
                            <div class="flex-col">
                              <xsl:call-template name="GRAOAddress">
                                <xsl:with-param name="GRAOAddress" select = "PDATA:PermanentAddress" />
                                <xsl:with-param name="AddressLabel" select = '"Постоянен/настоящ адрес"' />
                              </xsl:call-template>
                            </div>
                          </div>
                          <xsl:call-template name='Status'>
                            <xsl:with-param name='Status' select='PDATA:Status'/>
                          </xsl:call-template>
                        </div>
                      </li>
                    </xsl:for-each>
                  </ol>
                </div>
              </xsl:when>
            </xsl:choose>
            <xsl:choose>
              <xsl:when test='APP:Owners/APP:EntityData'>
                <div class="document-section">
                  <h3 class="section-title">Справка за юридически лица в ТРРЮЛНЦ / Регистър БУЛСТАТ</h3>
                  <ol class="list-field list-counter">
                    <xsl:for-each select="APP:Owners/APP:EntityData">
                      <li>
                        <div class="row-table">
                          <div class="flex-row">
                            <div class="flex-col">
                              <sapn class="label">ЕИК/БУЛСТАТ:</sapn>&#160;<xsl:value-of  select="EDATA:Identifier"/>
                            </div>
                          </div>
                          <div class="flex-row">
                            <div class="flex-col">
                              <sapn class="label">Кратко наименование:</sapn>&#160;<xsl:value-of  select="EDATA:Name"/>
                            </div>
                          </div>
                          <div class="flex-row">
                            <div class="flex-col-6">
                              <sapn class="label">Пълно наименование:</sapn>&#160;<xsl:value-of  select="EDATA:FullName"/>
                            </div>
                            <div class="flex-col-6">
                              <sapn class="label">Наименование на латиница:</sapn>&#160;<xsl:value-of  select="EDATA:NameTrans"/>
                            </div>
                          </div>
                          <div class="flex-row">
                            <div class="flex-col">
                              <xsl:call-template name="GRAOAddress">
                                <xsl:with-param name="GRAOAddress" select = "EDATA:EntityManagmentAddress" />
                                <xsl:with-param name="AddressLabel" select = '"Седалище и адрес на управление"' />
                              </xsl:call-template>
                            </div>
                          </div>
                          <xsl:call-template name='Status'>
                            <xsl:with-param name='Status' select='EDATA:Status'/>
                          </xsl:call-template>
                        </div>
                      </li>
                    </xsl:for-each>
                  </ol>
                </div>
              </xsl:when>
            </xsl:choose>
          </div>
          <div class="document-section">
            <div class="flex-row row-table">
              <div class="flex-col">
                <p>
                  Дата:&#160;<xsl:value-of  select="ms:format-date(APP:DocumentReceiptOrSigningDate, 'dd.MM.yyyy г.')"/>
                </p>
              </div>
            </div>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>