<xsl:stylesheet version="1.0" xmlns:APP="http://ereg.egov.bg/segment/R-3335"
          xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
          xmlns:R="http://ereg.egov.bg/segment/0009-000015"
          xmlns:ACURI="http://ereg.egov.bg/segment/0009-000044"
          xmlns:E="http://ereg.egov.bg/segment/0009-000013"
          xmlns:DURI="http://ereg.egov.bg/segment/0009-000001"
          xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
          xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
          xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
          xmlns:PD="http://ereg.egov.bg/segment/R-3037"
          xmlns:ms="urn:schemas-microsoft-com:xslt" xsi:type="xsl:transform" >

  <xsl:output omit-xml-declaration="yes" method="html"/>
  <xsl:include href="./KATBaseTemplatesNewDesign.xslt"/>
  <xsl:param name="SignatureXML"></xsl:param>
  <xsl:template match="APP:NotificationForTemporaryRegistrationPlates">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="Head"/>
      <body cz-shortcut-listen="true">
        <div class="print-document flex-container">
          <div class="document-section document-header">
            <p class="text-center text-lead text-uppercase">
              МИНИСТЕРСТВО НА ВЪТРЕШНИТЕ РАБОТИ
            </p>
            <p class="text-center text-bold text-uppercase">
              Главна Дирекция Национална Полиция
            </p>
            <hr/>
            <div class="flex-row">
              <div class="flex-col">
                <p class="text-left document-subtitle">
                  Рег. №: <xsl:value-of select="APP:DocumentURI/DURI:RegisterIndex" />-<xsl:value-of select="APP:DocumentURI/DURI:SequenceNumber" />-<xsl:value-of  select="ms:format-date(APP:DocumentURI/DURI:ReceiptOrSigningDate , 'dd.MM.yyyy')"/>
                </p>
              </div>
              <div class="flex-col">
                <p class="text-bold text-uppercase">
                  ДО<br/>ДИРЕКТОРА НА<br/>
                  <xsl:value-of select="APP:IssuingPoliceDepartment/PD:PoliceDepartmentName" />
                </p>
                <p class="text-bold text-uppercase">
                  КОПИЕ: ДО <br/>
                  <xsl:value-of select="APP:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/R:Entity/E:Name"/><br/>
                  <xsl:call-template name="EntityAddress">
                    <xsl:with-param name="EntityAddress" select = "APP:EntityManagementAddress" />
                  </xsl:call-template>
                  <br/>
                  по заявление с вх. № <xsl:value-of select="APP:AISCaseURI/ACURI:DocumentURI/DURI:RegisterIndex" />-<xsl:value-of select="APP:AISCaseURI/ACURI:DocumentURI/DURI:SequenceNumber" />-<xsl:value-of  select="ms:format-date(APP:AISCaseURI/ACURI:DocumentURI/DURI:ReceiptOrSigningDate , 'dd.MM.yyyy')"/>
                </p>
              </div>
            </div>
          </div>
          <div class="document-section">
            <p class="text-indent text-justify">
              Уважаеми Господин Директор,
            </p>
            <p class="text-indent text-justify">
              Главна дирекция „Национална полиция“ предоставя за ползване <b>
                <xsl:value-of  select="APP:CountOfSetsOfTemporaryPlates"/>&#160;/<xsl:value-of  select="APP:CountOfSetsOfTemporaryPlatesText"/>/ комплекта временни табели
              </b> с регистрационни номера на фирма <xsl:value-of select="APP:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/R:Entity/E:Name"/> с ЕИК <xsl:value-of select="APP:ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/R:Entity/E:Identifier"/>.
            </p>
            <p class="text-indent text-justify">Номерата се предоставят на фирмата за придвижване на автомобили до седалището на същата, до магазините за продажба в страната, за извършване на пробни пътувания от клиенти и до пунктовете на „Пътна полиция“ за регистрация на продадените автомобили.</p>
            <p class="text-indent text-justify">Предоставянето на табелите да се извърши по реда на Наредба № І-45/2000 г./ Обн. ДВ, бр. 31 от 2000 г.</p>
            <p class="text-indent text-justify">
              <b>Предоставен/и за ползване номер/а:</b>
              <br/>
              <xsl:value-of  select="APP:RegistrationNumbersForEachSet"/>
            </p>
          </div>
          <div class="document-section">
            <p class="text-indent text-justify">
              Срокът за ползване на временни табели с регистрационен номер е <b>12 /дванадесет/ месеца от датата на издаването им</b>.
            </p>
          </div>
          <div class="flex-row">
            <div class="flex-col"></div>
            <div class="flex-col">
              <p class="text-uppercase">
                Зам.-Директор: <br/>
                Старши комисар:
              </p>
            </div>
          </div>
          <div class="document-section">
            <div class="flex-row">
              <div class="flex-col">
                <p>
                  Дата:&#160;<xsl:value-of  select="ms:format-date(APP:DocumentReceiptOrSigningDate, 'dd.MM.yyyy г.')"/>
                </p>
              </div>
              <div class="flex-col">
                <p>
                  <xsl:value-of select="APP:Official/APP:PersonNames/NM:First/." />&#160;<xsl:value-of select="APP:Official/APP:PersonNames/NM:Last/." />
                </p>
                <xsl:call-template name="DocumentSignatures">
                  <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
                </xsl:call-template>
              </div>
            </div>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>