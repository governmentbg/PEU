<xsl:stylesheet version="1.0" xmlns:APP="http://ereg.egov.bg/segment/R-3341"
          xmlns:DURI="http://ereg.egov.bg/segment/0009-000001"
          xmlns:VRD="http://ereg.egov.bg/segment/R-3303"
          xmlns:VC="http://ereg.egov.bg/segment/R-3307"
          xmlns:PD="http://ereg.egov.bg/segment/R-3037"
          xmlns:S="http://ereg.egov.bg/segment/R-3301"
          xmlns:PDATA="http://ereg.egov.bg/segment/R-3300"
          xmlns:PBD="http://ereg.egov.bg/segment/0009-000008"
          xmlns:PI="http://ereg.egov.bg/segment/0009-000006"
          xmlns:ID="http://ereg.egov.bg/segment/0009-000099"
          xmlns:EDATA="http://ereg.egov.bg/segment/R-3302"
          xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
          xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
          xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:include href="./KATBaseTemplatesNewDesign.xslt"/>

  <xsl:template match="APP:ReportForChangingOwnershipV2">
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
          <xsl:for-each select="APP:VehicleData">
            <xsl:variable name="i" select="position()"/>
            <div class="document-section">
              <h2 class="section-title section-line">
                Справки по данни за ППС <br/>
                № <xsl:value-of select="APP:VehicleRegistrationData/VRD:RegistrationNumber"/>
              </h2>
              <div class="document-section">
                <h3 class="section-title">Идентифициране на ППС в АИС КАТ „Регистрация на ППС и собствениците им“</h3>
                <div class="row-table">
                  <div class="flex-row">
                    <div class="flex-col">
                      <span class="label">Регистрационен номер:</span>&#160;<xsl:value-of select="APP:VehicleRegistrationData/VRD:RegistrationNumber"/>
                    </div>
                  </div>
                  <div class="flex-row">
                    <div class="flex-col-6">
                      <span class="label">Рама:</span>&#160;<xsl:value-of select="APP:VehicleRegistrationData/VRD:IdentificationNumber"/>
                    </div>
                    <div class="flex-col-6">
                      <span class="label">Двигател:</span>&#160;<xsl:value-of select="APP:VehicleRegistrationData/VRD:EngineNumber"/>
                    </div>
                  </div>
                  <div class="flex-row">
                    <div class="flex-col-6">
                      <span class="label">Номер на свидетелство за регистрация на МПС:</span>&#160;<xsl:value-of select="APP:VehicleRegistrationData/VRD:RegistrationCertificateNumber"/>
                    </div>
                    <div class="flex-col-6">
                      <span class="label">Категория на ППС:</span>&#160;<xsl:value-of select="APP:VehicleRegistrationData/VRD:VehicleCategory/VC:Name" />
                    </div>
                  </div>
                  <div class="flex-row">
                    <div class="flex-col-6">
                      <span class="label">Дата на първа регистрация:</span>&#160;<xsl:value-of  select="ms:format-date(APP:VehicleRegistrationData/VRD:DateOfFirstReg, 'dd.MM.yyyy г.')"/>
                    </div>
                    <div class="flex-col-6">
                      <span class="label">Дата на следващ технически преглед:</span>&#160;<xsl:value-of  select="ms:format-date(APP:VehicleRegistrationData/VRD:NextVehicleInspection, 'dd.MM.yyyy г.')"/>
                    </div>
                  </div>
                  <div class="flex-row">
                    <div class="flex-col">
                      <span class="label">Структура на МВР, регистрирала ППС:</span>&#160;<xsl:value-of select="APP:VehicleRegistrationData/VRD:PoliceDepartment/PD:PoliceDepartmentName" />
                    </div>
                  </div>
                  <div class="flex-row">
                    <div class="flex-col">
                      <xsl:call-template name="Statuses">
                        <xsl:with-param name="Statuses" select="APP:VehicleRegistrationData/VRD:Statuses"/>
                        <xsl:with-param name="Status" select="APP:VehicleRegistrationData/VRD:Statuses/VRD:Status"/>
                      </xsl:call-template>
                    </div>
                  </div>
                </div>
              </div>
              <xsl:choose>
                <xsl:when test="APP:LocalTaxes/APP:Status">
                  <div class="document-section">
                    <h3 class="section-title">Централизирана информационна система за местни данъци и такси</h3>
                    <xsl:call-template name="Statuses">
                      <xsl:with-param name="Statuses" select="APP:LocalTaxes"/>
                      <xsl:with-param name="Status" select="APP:LocalTaxes/APP:Status"/>
                    </xsl:call-template>
                  </div>
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="APP:GuaranteeFund">
                  <div class="document-section">
                    <h3 class="section-title">Регистър застраховки "Гражданска отговорност (ГО)"</h3>
                    <xsl:choose>
                      <xsl:when test="APP:GuaranteeFund/APP:PolicyValidTo">
                        <p>
                          <span class="label">Дата на валидност на полицата:</span>&#160;<xsl:value-of  select="ms:format-date(APP:GuaranteeFund/APP:PolicyValidTo, 'dd.MM.yyyy г.')"/>
                        </p>
                      </xsl:when>
                    </xsl:choose>
                    <xsl:call-template name="Statuses">
                      <xsl:with-param name="Statuses" select="APP:GuaranteeFund"/>
                      <xsl:with-param name="Status" select="APP:GuaranteeFund/APP:Status"/>
                    </xsl:call-template>
                  </div>
                </xsl:when>
              </xsl:choose>
              <xsl:choose>
                <xsl:when test="APP:PeriodicTechnicalCheck">
                  <div class="document-section">
                    <h3 class="section-title">Регистър "Периодични технически прегледи"</h3>
                    <xsl:choose>
                      <xsl:when test="APP:PeriodicTechnicalCheck/APP:NextInspectionDate">
                        <p>
                          <span class="label">Дата на следващ преглед:</span>&#160;<xsl:value-of  select="ms:format-date(APP:PeriodicTechnicalCheck/APP:NextInspectionDate, 'dd.MM.yyyy г.')"/>
                        </p>
                      </xsl:when>
                    </xsl:choose>
                    <xsl:call-template name="Statuses">
                      <xsl:with-param name="Statuses" select="APP:PeriodicTechnicalCheck"/>
                      <xsl:with-param name="Status" select="APP:PeriodicTechnicalCheck/APP:Status"/>
                    </xsl:call-template>
                  </div>
                </xsl:when>
              </xsl:choose>
            </div>
            <div class="document-section">
              <h2 class="section-title section-line">Справки по данни за продавач/прехвърлител/дарител/съделител</h2>
              <xsl:choose>
                <xsl:when test="APP:OldOwners/APP:PersonData">
                  <div class="document-section">
                    <h3 class="section-title">Физически лица в НАИФ НРБЛД</h3>
                    <ol class="list-field list-counter">
                      <xsl:for-each select="APP:OldOwners/APP:PersonData">
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
                                <xsl:if test="PDATA:PersonBasicData/PBD:IdentityDocument/ID:IdentitityIssueDate != '0001-01-01'">
                                  <xsl:call-template name="IdentityDocumentType">
                                    <xsl:with-param name="IdentityDocumentType" select="PDATA:PersonBasicData/PBD:IdentityDocument/ID:IdentityDocumentType"/>
                                  </xsl:call-template>
                                </xsl:if>
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
                                <span class="label">Семейно положение:</span>
                                <xsl:call-template name="MaritalStatus">
                                  <xsl:with-param name="MaritalStatus" select="PDATA:MaritalStatus"/>
                                </xsl:call-template>
                                <br/>
                                <span class="document-note">Данните за семейно положение са информативни. Моля, извършете проверка за актуално семейно положение в ЕСГРАОН.</span>
                              </div>
                            </div>
                            <div class="flex-row">
                              <div class="flex-col">
                                <xsl:call-template name="GRAOAddress">
                                  <xsl:with-param name="GRAOAddress" select = "PDATA:PermanentAddress" />
                                  <xsl:with-param name="AddressLabel" select = '"Постоянен адрес"' />
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
                <xsl:when test="APP:OldOwners/APP:EntityData">
                  <div class="document-section">
                    <h3 class="section-title">Юридически лица в Търговски регистър / Регистър "Булстат"</h3>
                    <ol class="list-field list-counter">
                      <xsl:for-each select="APP:OldOwners/APP:EntityData">
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
                            <xsl:if test="EDATA:RecStatus !=''">
                              <div class="flex-row">
                                <div class="flex-col">
                                  <sapn class="label">Актуално състояние:</sapn>&#160;<xsl:value-of  select="EDATA:RecStatus"/>
                                </div>
                              </div>
                            </xsl:if>
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
              <h2 class="section-title section-line">Справки по данни за купувач/приемател/дарен/съделител</h2>
              <xsl:choose>
                <xsl:when test="APP:NewOwners/APP:PersonData">
                  <div class="document-section">
                    <h3 class="section-title">Физически лица в НАИФ НРБЛД</h3>
                    <ol class="list-field list-counter">
                      <xsl:for-each select="APP:NewOwners/APP:PersonData">
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
                                <xsl:if test="PDATA:PersonBasicData/PBD:IdentityDocument/ID:IdentitityIssueDate != '0001-01-01'">
                                  <xsl:call-template name="IdentityDocumentType">
                                    <xsl:with-param name="IdentityDocumentType" select="PDATA:PersonBasicData/PBD:IdentityDocument/ID:IdentityDocumentType"/>
                                  </xsl:call-template>
                                </xsl:if>
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
                                  <xsl:with-param name="AddressLabel" select = '"Постоянен адрес"' />
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
                <xsl:when test="APP:NewOwners/APP:EntityData">
                  <div class="document-section">
                    <h3 class="section-title">Юридически лица в Търговски регистър / Регистър "Булстат"</h3>
                    <ol class="list-field list-counter">
                      <xsl:for-each select="APP:NewOwners/APP:EntityData">
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
                            <xsl:if test="EDATA:RecStatus !=''">
                              <div class="flex-row">
                                <div class="flex-col">
                                  <sapn class="label">Актуално състояние:</sapn>&#160;<xsl:value-of  select="EDATA:RecStatus"/>
                                </div>
                              </div>
                            </xsl:if>
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
            <xsl:choose>
              <xsl:when test="position() != last()">
                <br/>
                <hr/>
                <br/>
              </xsl:when>
            </xsl:choose>
          </xsl:for-each>
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