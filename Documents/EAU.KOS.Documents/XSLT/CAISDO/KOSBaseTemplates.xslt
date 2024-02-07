<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:IDBD="http://ereg.egov.bg/segment/0009-000099"
                xmlns:ESA="http://ereg.egov.bg/segment/0009-000016"
                xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
                xmlns:AUT="http://ereg.egov.bg/segment/0009-000012"
                xmlns:REC="http://ereg.egov.bg/segment/0009-000015"
				        xmlns:P="http://ereg.egov.bg/segment/0009-000008"
                xmlns:E="http://ereg.egov.bg/segment/0009-000013"
                xmlns:DECL="http://ereg.egov.bg/segment//R-3136"
				        xmlns:NM="http://ereg.egov.bg/segment/0009-000005"
                xmlns:ADD="http://ereg.egov.bg/segment/0009-000139"
                xmlns:EASF="http://ereg.egov.bg/segment/0009-000153"
                xmlns:PD="http://ereg.egov.bg/segment/R-3037"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xslExtension="urn:XSLExtension">

  <!-- Custom Variables -->

  <!-- Head Css Styles -->

  <xsl:template name="Head">

    <head>
      <style>
        .digital-stamp {
        display: inline-block;
        vertical-align: middle;
        padding: 0.3125rem;
        margin-left: 1.875rem;
        font-family: Roboto, Arial, "Segoe UI", "Helvetica Neue", Verdana, sans-serif;
        border: 0.1875rem solid #a0cade;
        border-radius: 0.625rem;
        }

        .digital-stamp .digital-stamp-body {
        display: flex;
        flex-direction: row;
        align-items: center;
        justify-content: space-between;

        border: 0.125rem solid #80b8d3;
        border-radius: 0.375rem;
        text-align: left;
        }

        .digital-stamp .digital-stamp-name {
        flex: 0 1 auto;
        padding: 0.3125rem 0.3125rem 0.3125rem 0.625rem;
        }

        .digital-stamp .digital-stamp-data {
        flex: 0 1 auto;
        padding: 0.3125rem 0.625rem 0.3125rem 0.3125rem;
        }
        .digital-stamp .digital-stamp-name-text {
        font-size: 1.125rem;
        font-weight: bold;
        line-height: 1;
        max-width: 9rem;
        word-wrap: break-word;
        }
        .digital-stamp .digital-stamp-data-text {
        font-size: 0.625rem;
        line-height: 1;
        max-width: 6rem;
        word-wrap: break-word;
        }

      </style>
    </head>
  </xsl:template>


	<xsl:template name="HeadNew">
		<xsl:param name="title"/>
		<head>
			<meta charset="utf-8" />
			<title>
				<xsl:value-of select="$title"/>
			</title>
			<style>
				/* ----  Print  ---- */
				*, ::before, ::after {
				box-sizing: border-box; }

				body {
				margin: 0; }

				.digital-stamp {
				display: inline-block;
				vertical-align: middle;
				padding: 0.3125rem;
				font-family: Roboto, Arial, "Segoe UI", "Helvetica Neue", Verdana, sans-serif;
				border: 0.1875rem solid #a0cade;
				border-radius: 0.625rem; }
				.digital-stamp .digital-stamp-body {
				display: -ms-flexbox;
				display: flex;
				-ms-flex-direction: row;
				flex-direction: row;
				-ms-flex-align: center;
				align-items: center;
				-ms-flex-pack: justify;
				justify-content: space-between;
				border: 0.125rem solid #80b8d3;
				border-radius: 0.375rem;
				text-align: left; }
				.digital-stamp .digital-stamp-name {
				-ms-flex: 0 1 auto;
				flex: 0 1 auto;
				padding: 0.3125rem 0.3125rem 0.3125rem 0.625rem; }
				.digital-stamp .digital-stamp-data {
				-ms-flex: 0 1 auto;
				flex: 0 1 auto;
				padding: 0.3125rem 0.625rem 0.3125rem 0.3125rem; }
				.digital-stamp .digital-stamp-name-text {
				font-size: 1.125rem;
				font-weight: bold;
				line-height: 1;
				max-width: 9rem;
				word-wrap: break-word; }
				.digital-stamp .digital-stamp-data-text {
				font-size: 0.625rem;
				line-height: 1;
				max-width: 6rem;
				word-wrap: break-word; }

				.flex-container {
				padding-right: 0.625rem;
				padding-left: 0.625rem; }

				.row-table .flex-col-auto,
				.row-table .flex-col,
				.row-table .flex-col-1,
				.row-table .flex-col-2,
				.row-table .flex-col-3,
				.row-table .flex-col-4,
				.row-table .flex-col-5,
				.row-table .flex-col-6,
				.row-table .flex-col-7,
				.row-table .flex-col-8,
				.row-table .flex-col-9,
				.row-table .flex-col-10,
				.row-table .flex-col-11,
				.row-table .flex-col-12 {
				padding: 0  0.3125rem 0.625rem 0.3125rem; }

				.row-table + .row-table {
				margin-top: 0.75rem; }

				.flex-row {
				display: -ms-flexbox;
				display: flex;
				-ms-flex-wrap: nowrap;
				flex-wrap: nowrap;
				margin-right: -0.3125rem;
				margin-left: -0.3125rem; }

				.flex-col-auto,
				.flex-col,
				.flex-col-1,
				.flex-col-2,
				.flex-col-3,
				.flex-col-4,
				.flex-col-5,
				.flex-col-6,
				.flex-col-7,
				.flex-col-8,
				.flex-col-9,
				.flex-col-10,
				.flex-col-11,
				.flex-col-12 {
				position: relative;
				display: block;
				width: 100%;
				padding-right: 0.3125rem;
				padding-left: 0.3125rem; }

				.flex-col-auto {
				-ms-flex: 0 0 auto;
				flex: 0 0 auto;
				width: auto;
				max-width: 100%; }

				.flex-col {
				-ms-flex-preferred-size: 0;
				flex-basis: 0;
				-ms-flex-positive: 1;
				flex-grow: 1;
				max-width: 100%; }

				.flex-col-1 {
				-ms-flex: 0 0 8.33%;
				flex: 0 0 8.33%;
				max-width: 8.33%; }

				.flex-col-2 {
				-ms-flex: 0 0 16.66%;
				flex: 0 0 16.66%;
				max-width: 16.66%; }

				.flex-col-3 {
				-ms-flex: 0 0 25%;
				flex: 0 0 25%;
				max-width: 25%; }

				.flex-col-4 {
				-ms-flex: 0 0 33.33%;
				flex: 0 0 33.33%;
				max-width: 33.33%; }

				.flex-col-5 {
				-ms-flex: 0 0 41.66%;
				flex: 0 0 41.66%;
				max-width: 41.66%; }

				.flex-col-6 {
				-ms-flex: 0 0 50%;
				flex: 0 0 50%;
				max-width: 50%; }

				.flex-col-7 {
				-ms-flex: 0 0 58.33%;
				flex: 0 0 58.33%;
				max-width: 58.33%; }

				.flex-col-8 {
				-ms-flex: 0 0 66.66%;
				flex: 0 0 66.66%;
				max-width: 66.66%; }

				.flex-col-9 {
				-ms-flex: 0 0 75%;
				flex: 0 0 75%;
				max-width: 75%; }

				.flex-col-10 {
				-ms-flex: 0 0 83.33%;
				flex: 0 0 83.33%;
				max-width: 83.33%; }

				.flex-col-11 {
				-ms-flex: 0 0 91.66%;
				flex: 0 0 91.66%;
				max-width: 91.66%; }

				.flex-col-12 {
				-ms-flex: 0 0 100%;
				flex: 0 0 100%;
				max-width: 100%; }

				.flex-items-center {
				-ms-flex-align: center;
				align-items: center; }

				.print-document {
				width: 720px;
				margin: 0 auto;
				font-family: 'Times New Roman', Times, serif;
				font-size: 16px; }
				.print-document .document-title {
				font-size: 1.5em;
				font-weight: bold;
				margin: 1.875em 0 0.875em 0; }
				.print-document .document-title + .document-subtitle {
				margin: -.875em 0 1.25em 0; }
				.print-document .document-subtitle {
				font-size: 1.125em;
				font-weight: bold; }
				.print-document .document-section {
				margin-bottom: 1rem; }
				.print-document .document-section:not(:last-child) {
				margin-bottom: 2rem; }
				.print-document .document-section .section-title {
				font-size: 1.125em;
				font-weight: bold;
				margin-bottom: 0.625em; }
				.print-document .document-section .section-line {
				padding-bottom: 0.625em;
				border-bottom: 1px solid gray; }
				.print-document .document-section .document-section:not(:last-child) {
				margin-bottom: 1rem; }
				.print-document .document-section .document-section .section-title {
				font-size: 1.125em;
				font-weight: bold;
				font-style: italic;
				margin-bottom: 0.5em; }
				.print-document .document-section .document-section .document-section .section-title {
				font-size: 1em;
				font-weight: bold;
				font-style: normal;
				margin-bottom: 0.5em; }
				.print-document .document-note {
				font-size: .85em;
				font-style: italic; }
				.print-document b {
				font-weight: bold; }
				.print-document p {
				margin-top: 0.625rem;
				margin-bottom: 0.625rem; }
				.print-document .table-margins {
				margin-top: 0.125rem;
				margin-bottom: 0.125rem; }
				.print-document .text-lead {
				font-size: 1.125em;
				font-weight: bold; }
				.print-document .text-uppercase {
				text-transform: uppercase !important; }
				.print-document .text-bold {
				font-weight: bold !important; }
				.print-document .text-center {
				text-align: center !important; }
				.print-document .text-left {
				text-align: left !important; }
				.print-document .text-right {
				text-align: right !important; }
				.print-document .text-justify {
				text-align: justify !important; }
				.print-document .text-indent {
				text-indent: 1.5em !important; }
				.print-document .text-underline {
				text-decoration: underline; }
				.print-document .list-unstyled {
				list-style: none;
				padding-left: 1.5em; }
				.print-document .text-number {
				padding: 0.125rem 0;
				max-width: 4em;
				min-width: 3em; }
				.print-document ol,
				.print-document ul {
				padding-left: 2.5em;
				margin-top: 0.625em;
				margin-bottom: 0.625em; }
				.print-document li {
				margin-bottom: 0.3125rem; }
				.print-document .list-field > li {
				margin-bottom: 0; }
				.print-document .list-field > li:not(:last-child) {
				border-bottom: 1px dotted gray;
				padding-bottom: 0.3125em;
				margin-bottom: 0.625em; }
				.print-document .list-counter {
				padding-left: 0;
				position: relative;
				list-style: none;
				counter-reset: list-counter; }
				.print-document .list-counter > li:not(:only-child) {
				padding-left: 2em;
				counter-increment: list-counter; }
				.print-document .list-counter > li:not(:only-child)::before {
				content: counter(list-counter) ".";
				position: absolute;
				left: 0; }
				.print-document hr {
				border: none;
				border-bottom: 2px solid gray; }
				.print-document .table {
				width: 100%;
				border-spacing: 0;
				border-collapse: collapse;
				text-indent: 0; }
				.print-document .table th {
				text-align: left; }
				.print-document .table td, .print-document .table th {
				vertical-align: top;
				padding: 0.3125rem 0.3125rem; }
				.print-document .table-borderless {
				border: none; }
				.print-document .table-borderless td, .print-document .table-borderless th {
				border: none;
				padding: 0  0.625rem 0.625rem 0; }
				.print-document .photo {
				border: 1px solid gray;
				max-width: 130px;
				max-height: 160px; }

				@media print {
				.print-document {
				width: 100%; } }

				/* ----  /Print  ---- */

			</style>
		</head>
	</xsl:template>

  <!-- Base Templates -->

  <xsl:template name="IssuingPoliceDepartmentHeader">
    <xsl:param name="IssuingPoliceDepartment"/>
    ДО<br/>
    <xsl:choose>
      <xsl:when test="$IssuingPoliceDepartment/PD:PoliceDepartmentCode = 3286">
        ДИРЕКТОРА НА<br/>
      </xsl:when>
      <xsl:otherwise>
        НАЧАЛНИКА НА<br/>
      </xsl:otherwise>
    </xsl:choose>
    <xsl:value-of select="$IssuingPoliceDepartment/PD:PoliceDepartmentName" />
  </xsl:template>

  <xsl:template name="IssuingPoliceDepartmentDirectTo">
    <xsl:param name="IssuingPoliceDepartment"/>
    <xsl:choose>
      <xsl:when test="$IssuingPoliceDepartment/PD:PoliceDepartmentCode = 3286">
        &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<b>Господин Директор,</b>
      </xsl:when>
      <xsl:otherwise>
        &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<b>Господин Началник,</b>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ApplicationElectronicServiceApplicantAndRepresentativeWithAddress">
    <xsl:param name="ElectronicServiceApplicant"/>
    <xsl:param name="Phone"/>
    <xsl:param name="PersonAddress"/>

    <tr>
      <td colspan ="2">
        <xsl:call-template name="ApplicationElectronicServiceApplicant">
          <xsl:with-param name="ElectronicServiceApplicant" select = "$ElectronicServiceApplicant" />
        </xsl:call-template>
      </td>
    </tr>
    <tr>
      <td colspan ="2">
        <xsl:call-template name="PersonGRAOAddress">
          <xsl:with-param name="PersonAddress" select = "$PersonAddress" />
        </xsl:call-template>
      </td>
    </tr>
    <tr>
      <td colspan ="2">
        <xsl:call-template name="ApplicationElectronicServiceApplicantPhone">
          <xsl:with-param name="Phone" select = "$Phone" />
        </xsl:call-template>
      </td>
    </tr>
    <tr>
      <td colspan ="2">
        <xsl:call-template name="ApplicationElectronicServiceApplicantEmail">
          <xsl:with-param name="ElectronicServiceApplicant" select = "$ElectronicServiceApplicant" />
        </xsl:call-template>
      </td>
    </tr>

    <xsl:call-template name="ApplicationElectronicServiceApplicantRepresentative">
      <xsl:with-param name="ElectronicServiceApplicant" select = "$ElectronicServiceApplicant" />
    </xsl:call-template>
  </xsl:template>
  
  <xsl:template name="ApplicationElectronicServiceApplicantAndRepresentative">
    <xsl:param name="ElectronicServiceApplicant"/>
    <xsl:param name="Phone"/>
    
    <tr>
      <td colspan ="2">
        <xsl:call-template name="ApplicationElectronicServiceApplicant">
          <xsl:with-param name="ElectronicServiceApplicant" select = "$ElectronicServiceApplicant" />
        </xsl:call-template>
      </td>
    </tr>
    <tr>
      <td colspan ="2">
        <xsl:call-template name="ApplicationElectronicServiceApplicantPhone">
          <xsl:with-param name="Phone" select = "$Phone" />
        </xsl:call-template>
      </td>
    </tr>
    <tr>
      <td colspan ="2">
        <xsl:call-template name="ApplicationElectronicServiceApplicantEmail">
          <xsl:with-param name="ElectronicServiceApplicant" select = "$ElectronicServiceApplicant" />
        </xsl:call-template>
      </td>
    </tr>

    <xsl:call-template name="ApplicationElectronicServiceApplicantRepresentative">
      <xsl:with-param name="ElectronicServiceApplicant" select = "$ElectronicServiceApplicant" />
    </xsl:call-template>
  </xsl:template>
  
  <xsl:template name="ApplicationElectronicServiceApplicant">
    <xsl:param name="ElectronicServiceApplicant"/>
    От&#160;
    <xsl:choose>
      <xsl:when test="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">

        <xsl:call-template name="PersonNames">
          <xsl:with-param name="Names" select = "$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Names" />
        </xsl:call-template>

      </xsl:when>
      <xsl:otherwise>

        <xsl:call-template name="PersonNames">
          <xsl:with-param name="Names" select = "$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names" />
        </xsl:call-template>

      </xsl:otherwise>
    </xsl:choose>

    <xsl:choose>
      <xsl:when test="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
        <xsl:value-of  select="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Name/."/>
      </xsl:when>
    </xsl:choose>

    <xsl:choose>
      <xsl:when test="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity">
        ,&#160;ЕИК/БУЛСТАТ:&#160;
        <xsl:value-of  select="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Entity/E:Identifier/."/>,
      </xsl:when>
      <xsl:otherwise>
        ,&#160;ЕГН/ЛНЧ/ЛН:&#160;
        <xsl:value-of  select="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:Identifier/."/>,
      </xsl:otherwise>
    </xsl:choose><xsl:call-template name="IdentityDocument">
      <xsl:with-param name="IdentityDocument" select = "$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Recipient/REC:Person/P:IdentityDocument" />
    </xsl:call-template>

  </xsl:template>

  <xsl:template name="ApplicationElectronicServiceApplicantRepresentative">
    <xsl:param name="ElectronicServiceApplicant"/>
    <xsl:choose>
      <xsl:when test="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:AuthorQualityType != 'R-1001'">
        <tr>
          <td colspan ="2">
            с пълномощник/представител:&#160;<xsl:call-template name="PersonNames">
              <xsl:with-param name="Names" select = "$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Names" />
            </xsl:call-template>,&#160;ЕГН/ЛНЧ/ЛН:&#160;<xsl:value-of  select="$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:Identifier/."/>,
            <xsl:call-template name="IdentityDocument">
              <xsl:with-param name="IdentityDocument" select = "$ElectronicServiceApplicant/ESA:RecipientGroup/ESA:Author/AUT:Person/P:IdentityDocument" />
            </xsl:call-template>

          </td >
        </tr>

      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="PersonNames">
    <xsl:param name="Names"/>
    <xsl:value-of  select="$Names/NM:First/."/>&#160;<xsl:choose><xsl:when test="$Names/NM:Middle/."><xsl:value-of  select="$Names/NM:Middle/."/>&#160;</xsl:when></xsl:choose><xsl:value-of  select="$Names/NM:Last/."/>
  </xsl:template>

  <xsl:template name="PersonGRAOAddress">
    <xsl:param name="PersonAddress"/>
      Адрес:&#160;
      <xsl:choose>
        <xsl:when test="$PersonAddress/PA:DistrictGRAOName">
          Обл.&#160;<xsl:value-of  select="$PersonAddress/PA:DistrictGRAOName"/>&#160;
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="$PersonAddress/PA:MunicipalityGRAOName">
          Общ.&#160;<xsl:value-of  select="$PersonAddress/PA:MunicipalityGRAOName"/>&#160;
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="$PersonAddress/PA:SettlementGRAOName">
          &#160;<xsl:value-of  select="$PersonAddress/PA:SettlementGRAOName"/>&#160;<br/>
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="$PersonAddress/PA:StreetText">
          бул./ул./ж.к.&#160;<xsl:value-of  select="$PersonAddress/PA:StreetText"/>&#160;
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="$PersonAddress/PA:BuildingNumber">
          №/бл.&#160;<xsl:value-of  select="$PersonAddress/PA:BuildingNumber"/>&#160;
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="$PersonAddress/PA:Entrance">
          вх.&#160;<xsl:value-of  select="$PersonAddress/PA:Entrance"/>&#160;
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="$PersonAddress/PA:Floor">
          ет.&#160;<xsl:value-of  select="$PersonAddress/PA:Floor"/>&#160;
        </xsl:when>
      </xsl:choose>
      <xsl:choose>
        <xsl:when test="$PersonAddress/PA:Apartment">
          ап.&#160;<xsl:value-of  select="$PersonAddress/PA:Apartment"/>&#160;
        </xsl:when>
      </xsl:choose>
    
  </xsl:template>
  
  <xsl:template name="ApplicationElectronicServiceApplicantPhone">
    <xsl:param name="Phone"/>
    Телефон:&#160;<xsl:choose><xsl:when test="$Phone"><xsl:value-of  select="$Phone"/></xsl:when></xsl:choose>
  </xsl:template>

  <xsl:template name="ApplicationElectronicServiceApplicantEmail">
    <xsl:param name="ElectronicServiceApplicant"/>
    <xsl:choose>
      <xsl:when test="$ElectronicServiceApplicant/ESA:EmailAddress">
        Адрес на електронна поща:&#160;<xsl:value-of  select="$ElectronicServiceApplicant/ESA:EmailAddress"/>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="IdentityDocumentName">
    <xsl:param name="IdentityDocument"/>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000087'">
        Лична карта
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000088'">
        Паспорт
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000089'">
        Дипломатически паспорт
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000090'">
        Служебен паспорт
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000091'">
        Моряшки паспорт
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000092'">
        Военна карта за самоличност
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000093'">
        Свидетелство за управление на моторно превозно средство
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000094'">
        Временен паспорт
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000095'">
        Служебен открит лист за преминаване на границата
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000097'">
        Карта на бежанец
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000099'">
        Карта на чужденец с хуманитарен статут
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000098'">
        Карта на чужденец, получил убежище
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000121'">
        Разрешение за пребиваване
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
      <xsl:when test="$IdentityDocument/IDBD:IdentityDocumentType = '0006-000122'">
        Удостоверение за пребиваване на гражданин на ЕС
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="IdentityDocument">
    <xsl:param name="IdentityDocument"/>
    <xsl:choose>
      <xsl:when test="$IdentityDocument">
        <xsl:call-template name="IdentityDocumentName">
          <xsl:with-param name="IdentityDocument" select = "$IdentityDocument" />
        </xsl:call-template> №:&#160;<xsl:value-of  select="$IdentityDocument/IDBD:IdentityNumber"/>,

        изд. на&#160;<xsl:choose><xsl:when test="$IdentityDocument/IDBD:IdentitityIssueDate"><xsl:value-of select="xslExtension:FormatDate($IdentityDocument/IDBD:IdentitityIssueDate, 'dd.MM.yyyy')"/>г.</xsl:when>
        </xsl:choose>&#160;от&#160;<xsl:value-of  select="$IdentityDocument/IDBD:IdentityIssuer"/>

      </xsl:when>
    </xsl:choose>
  </xsl:template>

  
  <xsl:template name="ServiceResultReceiptMethod">
    <xsl:param name="ServiceResultReceiptMethod"/>
    <tr>
      <td colspan = "2">
        <b>Начин на получаване на услугата:&#160;</b>
        <xsl:choose>
          <xsl:when test="$ServiceResultReceiptMethod = '0006-000076'">
            чрез електронна поща/уеб базирано приложение
          </xsl:when>
        </xsl:choose>
        <xsl:choose>
          <xsl:when test="$ServiceResultReceiptMethod = '0006-000077'">
            на гише в структурата на МВР, изпълнител на услугата
          </xsl:when>
        </xsl:choose>
      </td>
    </tr>
  </xsl:template>
  
  <xsl:template name="AgreementToReceiveERefusal">
    <xsl:param name="AgreementToReceiveERefusal"/>
    <xsl:choose>
      <xsl:when test = "$AgreementToReceiveERefusal = 'true'">
        <tr>
          <td colspan = "2">
            В случай на отказ от предоставяне на услугата, желая да получа отказа като електронен документ, съгласно чл.25, ал.2 от Закона за електронното управление.
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="DocumentSignatures">
    <xsl:param name="Signatures"/>
    <xsl:if test="$Signatures">
      <xsl:for-each select="$Signatures/Signature">
        <xsl:call-template name="DocumentSignature">
          <xsl:with-param name="Signature" select = "$Signatures/Signature" />
        </xsl:call-template>
      </xsl:for-each>
    </xsl:if>
  </xsl:template>

  
  <xsl:template name="Declarations">
    <xsl:param name="Declarations"/>
    <xsl:param name="Declaration"/>

    <xsl:choose>
      <xsl:when test = "$Declarations">
        <xsl:for-each select="$Declaration">
          <xsl:call-template name="Declaration">
            <xsl:with-param name="Declaration" select = "." />
          </xsl:call-template>
        </xsl:for-each>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="Declaration">
    <xsl:param name="Declaration"/>
    <xsl:choose>
      <xsl:when test="$Declaration/DECL:IsDeclarationFilled = 'true'">
        <tr>
          <td colspan="2">
            <xsl:value-of  select="$Declaration/DECL:DeclarationName" disable-output-escaping="yes"/>
          </td>
        </tr>
        <xsl:choose>
          <xsl:when test="$Declaration/DECL:FurtherDescriptionFromDeclarer">
            <tr>
              <td colspan="2">
                Декларирам (допълнително описание на обстоятелствата по декларацията):<xsl:value-of  select="$Declaration/DECL:FurtherDescriptionFromDeclarer"/>
              </td>
            </tr>
            <tr>
              <td colspan="2">
                <xsl:value-of  select="$Declaration/DECL:FurtherDescriptionFromDeclarer"/>
              </td>
            </tr>
          </xsl:when>
        </xsl:choose>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="AttachedDocuments">
    <xsl:param name="AttachedDocuments"/>
    <xsl:param name="AttachedDocument"/>
    <xsl:choose>
      <xsl:when test = "$AttachedDocuments">
        <tr>
          <td colspan="2">
            Приложени документи:
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <ol>
              <xsl:for-each select="$AttachedDocument">
                <li>
                  <xsl:value-of select="ADD:AttachedDocumentDescription" />
                </li>
              </xsl:for-each>
            </ol>
          </td>
        </tr>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="ElectronicAdministrativeServiceFooterDate">
    <xsl:param name="ElectronicAdministrativeServiceFooter"/>
    Дата:&#160;<xsl:call-template name="Date">
          <xsl:with-param name="Date" select = "$ElectronicAdministrativeServiceFooter/EASF:ApplicationSigningTime" />
        </xsl:call-template>
  </xsl:template>

  <xsl:template name="Date">
    <xsl:param name="Date"/>
    <xsl:choose>
      <xsl:when test="$Date">
        <xsl:value-of select="xslExtension:FormatDate($Date, 'dd.MM.yyyy')"/>г.
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  
  <xsl:template name="DocumentSignature">
    <xsl:param name="Signature"/>
    <xsl:if test="$Signature">
      <div class="digital-stamp">
        <div class="digital-stamp-body">
          <div class="digital-stamp-name">
            <div class="digital-stamp-name-text">
              <xsl:value-of select="xslExtension:ExtractFirstGroup(SigningCertificateData/Subject/., 'CN=(.+?),')"/>
            </div>
          </div>
          <div class="digital-stamp-data">
            <div class="digital-stamp-data-text">
              Digitally signed by<br/>
              <xsl:value-of select="xslExtension:ExtractFirstGroup(SigningCertificateData/Subject/., 'CN=(.+?),')"/><br/>
              Date:&#32;
              <xsl:value-of disable-output-escaping="yes" select="xslExtension:FormatDate(SignatureTime/., 'yyyy.MM.dd&lt;\/br&gt;HH:mm:ss zzz')"/>
            </div>
          </div>
        </div>
      </div>
    </xsl:if>
  </xsl:template>

  <xsl:template name="ApplicationElectronicAdministrativeServiceFooter">
    <xsl:param name="ElectronicAdministrativeServiceFooter"/>
    <xsl:param name="SignatureXML"/>

    <tr>
      <td width="50%">
        <xsl:call-template name="ElectronicAdministrativeServiceFooterDate">
          <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "$ElectronicAdministrativeServiceFooter" />
        </xsl:call-template>
      </td>
      <td width="50%">
        <xsl:call-template name="DocumentSignatures">
          <xsl:with-param name="Signatures" select = "$SignatureXML/DocumentSignatures" />
        </xsl:call-template>
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="ApplicationElectronicAdministrativeServiceFooterLite">
    <xsl:param name="ElectronicAdministrativeServiceFooter"/>

    <tr>
      <td width="50%">
        <xsl:call-template name="ElectronicAdministrativeServiceFooterDate">
          <xsl:with-param name="ElectronicAdministrativeServiceFooter" select = "$ElectronicAdministrativeServiceFooter" />
        </xsl:call-template>
      </td>
      <td width="50%">
      </td>
    </tr>
  </xsl:template>
  
</xsl:stylesheet>