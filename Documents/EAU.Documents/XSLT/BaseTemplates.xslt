<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ms="urn:schemas-microsoft-com:xslt"
				        xmlns:dt="urn:schemas-microsoft-com:datatypes"
                xmlns:DU="http://ereg.egov.bg/segment/0009-000001"
                xmlns:ESPBD="http://ereg.egov.bg/segment/0009-000002"
                xmlns:DTU="http://ereg.egov.bg/segment/0009-000003"
                xmlns:PN="http://ereg.egov.bg/segment/0009-000005"
                xmlns:PI="http://ereg.egov.bg/segment/0009-000006"
                xmlns:FCN="http://ereg.egov.bg/segment/0009-000007"
                xmlns:PBD="http://ereg.egov.bg/segment/0009-000008"
                xmlns:FCPB="http://ereg.egov.bg/segment/0009-000009"
                xmlns:FCID="http://ereg.egov.bg/segment/0009-000010"
                xmlns:FCBD="http://ereg.egov.bg/segment/0009-000011"
                xmlns:ESAu="http://ereg.egov.bg/segment/0009-000012"
                xmlns:EBD="http://ereg.egov.bg/segment/0009-000013"
                xmlns:FEBD="http://ereg.egov.bg/segment/0009-000014"
                xmlns:ESR="http://ereg.egov.bg/segment/0009-000015"
                xmlns:ESAp="http://ereg.egov.bg/segment/0009-000016"
                xmlns:ROU="http://ereg.egov.bg/segment/0009-000022"
                xmlns:RDU="http://ereg.egov.bg/segment/0009-000044"
                xmlns:PA="http://ereg.egov.bg/segment/0009-000094"
                xmlns:POB="http://ereg.egov.bg/segment/0009-000095"
                xmlns:POBА="http://ereg.egov.bg/segment/0009-000096"
                xmlns:C="http://ereg.egov.bg/segment/0009-000133"
                xmlns:CRBD="http://ereg.egov.bg/segment/0009-000135"
                xmlns:R="http://ereg.egov.bg/segment/0009-000142"
                xmlns:EMA="http://ereg.egov.bg/segment/0009-000172"
                xmlns:ID="http://ereg.egov.bg/segment/R-2114"
                xmlns:ICD="http://ereg.egov.bg/segment/R-2118"
                xmlns:RII="http://ereg.egov.bg/segment/R-3010"
                xmlns:P="http://ereg.egov.bg/segment/R-3312"
                xmlns:E="http://ereg.egov.bg/segment/R-3314"
                xmlns:GC="http://ereg.egov.bg/segment/R-4015">

  <!-- Custom Variables -->

  <xsl:variable name="space">
    <xsl:value-of select="' '"></xsl:value-of>
  </xsl:variable>

  <!-- Head Css Styles -->

  <xsl:template name="Head">

    <head>
      <style>
        body, table
        {
        font-family: Verdana;
        font-size: 10pt;
        color: black;
        text-align: justify;
        }
        h1
        {
        font-size: 12pt;
        color: black;
        text-align: center;
        }
        h2
        {
        padding-top:5px;
        text-align:center;
        vertical-align:middle;
        font-size: 10pt;
        }
        h3
        {
        font-variant: small-caps;
        text-align:center;
        }
        h4
        {
        font-size: 12pt;
        font-variant: small-caps;
        text-align: center;
        }
        hr
        {
        border: 1px solid #000000;
        }
        .mainCell
        {
        vertical-align:top;
        }
        .mainTable
        {
        vertical-align:top;
        }
        .dataLabel
        {
        }
        .datalabel2 {
        color: #0000CC;
        }
        .literal
        {
        font-weight: normal;
        }
        .left
        {
        font-size: 8pt;
        padding: 3px 5px 3px 5px;
        vertical-align: top;
        width: 90px;
        text-align: left;
        }
        .right
        {
        font-size: 8pt;
        padding: 3px 5px 3px 5px;
        vertical-align: top;
        }
        .signTable
        {
        border: 1px solid #C0C0C0;
        padding: 0px 5px 5px 5px;
        vertical-align: top;
        font-size: 8pt;
        }
        .mini
        {
        font-family: Verdana;
        font-size: 8pt;
        font-variant: normal;
        font-weight:normal;
        }
        .header
        {
        background-color: #F0F0F0;
        color: #333333;
        font-style: italic;
        }
        .left_mini
        {
        width: 100px;
        text-align: center;
        }
        .legend_gray
        {
        font-weight: bold;
        color: #676767;
        }
        .greeBorders
        {
        background-position: inherit;
        padding: 0px 5px 5px 5px;
        border-right: #b0b0b0 1px solid;
        border-top: #b0b0b0 1px solid;
        border-left: #b0b0b0 1px solid;
        border-bottom: #b0b0b0 1px solid;
        background-image: inherit;
        background-repeat: inherit;
        background-attachment: inherit;
        background-color: #FFFFFF;
        margin-top: 10px;
        }
        #content {
        margin: 15mm 15mm 10mm 25mm;
        }

        .uppercase{
        text-transform: uppercase;
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

  <xsl:template name="Logo">
    <table>
      <tr>
        <td valign="top" align="center">
          <xsl:element name="img">
            <xsl:attribute name="src"><xsl:value-of select="$ApplicationPath"/>/Content/images/logo.png</xsl:attribute>
          </xsl:element>
        </td>
        <td>
          РЕПУБЛИКА БЪЛГАРИЯ
          <br />
          Населено място/район
          <br />
          Община
          <br />
          Област
        </td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template name="Logo2">
    <table align="center">
      <tr>
        <td valign="top" align="center">
          <xsl:element name="img">
            <xsl:attribute name="src"><xsl:value-of select="$ApplicationPath"/>/Content/images/logo.png</xsl:attribute>
          </xsl:element>
        </td>
      </tr>
      <tr>
        <td>
          <h1>ОБЩИНА ПЛОВДИВ</h1>
        </td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template name="Logo3">
    
    <xsl:element name="img">
      <xsl:attribute name="src"><xsl:value-of select="$ApplicationPath"/>/Content/images/logo.png</xsl:attribute>
    </xsl:element>
    <!--<img  src="../XSLs/images/logo_2.png" style="margin-left:140px;"/>-->
    <br></br>
  </xsl:template>


  <!-- Base Templates -->

  <xsl:template name="StringWithSpaceIfExists">
    <xsl:param name="String"/>
    <xsl:if test="$String">
      <xsl:value-of select="$String" />
      <xsl:value-of select="$space" />
    </xsl:if>
  </xsl:template>

  <xsl:template name="FormatDate">
    <xsl:param name="DateTime"/>
    <xsl:variable name="year">
      <xsl:value-of select="substring($DateTime,1,4)"/>
    </xsl:variable>
    <xsl:variable name="day">
      <xsl:value-of select="substring($DateTime,9,2)"/>
    </xsl:variable>
    <xsl:variable name="month">
      <xsl:value-of select="substring($DateTime,6,2)"/>
    </xsl:variable>
    <xsl:variable name="hour">
      <xsl:value-of select="substring($DateTime,12,5)"/>
    </xsl:variable>
    <span class="dataLabel">
      <xsl:if test="$day and $month and $year">
        <xsl:value-of select="$day"/>
        <xsl:value-of select="'.'"/>
        <xsl:value-of select="$month"/>
        <xsl:value-of select="'.'"/>
        <xsl:value-of select="$year"/>
        <xsl:if test="$hour">
          <xsl:value-of select="' '"/>
          <xsl:value-of select="$hour"/>
        </xsl:if>
      </xsl:if>
    </span>
  </xsl:template>

  <xsl:template name="FormatDateOnly">
    <xsl:param name="DateTime"/>
    <span class="dataLabel">
      <xsl:value-of select="ms:format-date($DateTime, 'dd MMMM yyyy', 'BG-bg')"/>
    </span>
  </xsl:template>

  <xsl:template name="FormatDateWithTime">
    <xsl:param name="DateTime"/>
    <span class="dataLabel">
      <xsl:value-of select="ms:format-date($DateTime, 'dd MMMM yyyy', 'BG-bg')"/>
      <xsl:value-of select="$space"/>
      <xsl:value-of select="ms:format-time($DateTime, 'HH:mm:ss')"/>
    </span>
  </xsl:template>

  <xsl:template name="TransportType">
    <xsl:param name="Type"/>
    <span class="dataLabel">
      <xsl:choose>
        <xsl:when test="$Type='0006-000001'">
          уеб базирано приложение
        </xsl:when>
        <xsl:when test="$Type='0006-000002'">
          електронна поща
        </xsl:when>
        <xsl:when test="$Type='0006-000003'">
          физически носител
        </xsl:when>
        <xsl:when test="$Type='0006-000004'">
          единната среда за обмен на електронни документи
        </xsl:when>
        <xsl:otherwise></xsl:otherwise>
      </xsl:choose>
    </span>
  </xsl:template>

  <xsl:template name="ElectronicServiceProviderType">
    <xsl:param name="Type"/>
    <span class="dataLabel" >
      <xsl:choose>
        <xsl:when test="$Type='0006-000031'">
          Административен орган
        </xsl:when>
        <xsl:when test="$Type='0006-000032'">
          Лице, осъществяващо публични функции
        </xsl:when>
        <xsl:when test="$Type='0006-000033'">
          Организация, предоставящи обществени услуги
        </xsl:when>
        <xsl:otherwise></xsl:otherwise>
      </xsl:choose>
    </span>
  </xsl:template>

  <xsl:template name="Discrepancy">
    <xsl:param name="Type"/>
    <span class="dataLabel">
      <xsl:choose>
        <xsl:when test="$Type='0006-000005'">
          Подаваното заявление не е в нормативно установения формат
        </xsl:when>
        <xsl:when test="$Type='0006-000006'">
          Размерът на заявлението заедно с приложенията надвишава определения от административния орган размер за електронните административни услуги, предоставяни от съответната администрация
        </xsl:when>
        <xsl:when test="$Type='0006-000007'">
          Приложените към заявлението документи не са в нормативно установения формат
        </xsl:when>
        <xsl:when test="$Type='0006-000008'">
          Подаденото заявление и приложенията към него съдържат вируси или друг нежелан софтуер
        </xsl:when>
        <xsl:when test="$Type='0006-000009'">
          Подаденото заявление не съдържа уникален идентификатор на заявителя и на получателя на електронната административна услуга при законово изискване за идентификация
        </xsl:when>
        <xsl:when test="$Type='0006-000010'">
          Заявителят не е посочил електронен пощенски адрес
        </xsl:when>
        <xsl:when test="$Type='0006-000011'">
          Не е налице техническа възможност за достъп до съдържанието на подадения на физически носител електронен документ
        </xsl:when>
        <xsl:otherwise></xsl:otherwise>
      </xsl:choose>
    </span>
  </xsl:template>

  <xsl:template name="GuardianQuality">
    <xsl:param name="Type"/>
    <span class="dataLabel" >
      <xsl:choose>
        <xsl:when test="$Type='0006-000124'">
          Майка
        </xsl:when>
        <xsl:when test="$Type='0006-000125'">
          Баща
        </xsl:when>
        <xsl:when test="$Type='0006-000137'">
          Съпруг
        </xsl:when>
        <xsl:when test="$Type='0006-000138'">
          Съпруга
        </xsl:when>
        <xsl:otherwise></xsl:otherwise>
      </xsl:choose>
    </span>
  </xsl:template>

  <xsl:template name="TypesLegalRestrictions">
    <xsl:param name="Type"/>
    <span class="dataLabel" >
      <xsl:choose>
        <xsl:when test="$Type='R-1071'">
          Пълно запрещение
        </xsl:when>
        <xsl:when test="$Type='R-1072'">
          Ограничено запрещение
        </xsl:when>
        <xsl:when test="$Type='R-1073'">
          Лишен от родителски права
        </xsl:when>
        <xsl:when test="$Type='R-1074'">
          Ограничени родителски права
        </xsl:when>
        <xsl:otherwise></xsl:otherwise>
      </xsl:choose>
    </span>
  </xsl:template>

  <xsl:template name="SpeciesExchangeInsuranceIncome">
    <xsl:param name="Type"/>
    <span class="dataLabel">
      <xsl:choose>
        <xsl:when test="$Type='R-2159'">
          Деноминирани лева
        </xsl:when>
        <xsl:when test="$Type='R-2160'">
          Неденоминирани лева
        </xsl:when>
        <xsl:otherwise></xsl:otherwise>
      </xsl:choose>
    </span>
  </xsl:template>

  <!-- Rio Segments Templates -->

  <!-- 0009-000001 - DocumentURI -->
  <xsl:template name="DocumentURI">
    <xsl:value-of select="DU:RegisterIndex/." />-<xsl:value-of select="DU:SequenceNumber/." />-<xsl:call-template name="FormatDate">
      <xsl:with-param name="DateTime" select="DU:ReceiptOrSigningDate/." />
    </xsl:call-template>
  </xsl:template>

  <!-- 0009-000002 - ElectronicServiceProviderBasicData -->
  <xsl:template name="ElectronicServiceProviderBasicData">
    <xsl:for-each select="ESPBD:EntityBasicData">
      <xsl:call-template name="EntityBasicData" />
    </xsl:for-each>
  </xsl:template>

  <!-- 0009-000003 - DocumentTypeURI -->
  <xsl:template name="DocumentTypeURI">
    <span class="dataLabel">
      <xsl:value-of select="ROU:RegisterIndex/."/>-<xsl:value-of select="ROU:BatchNumber/."/>
    </span>
  </xsl:template>

  <!-- 0009-000006 - PersonIdentifier -->
  <xsl:template name="PersonIdentifier">
    <xsl:if test="PI:EGN">
      , ЕГН:
      <span class="dataLabel">
        <xsl:value-of select="."/>
      </span>
    </xsl:if>
    <xsl:if test="PI:LNCh">
      , ЛНЧ:
      <span class="dataLabel">
        <xsl:value-of select="."/>
      </span>
    </xsl:if>
  </xsl:template>

  <!-- 0009-000005 - PersonNames -->
  <xsl:template name="PersonNames">

    <xsl:value-of select="PN:First/." />
    <xsl:value-of select="$space" />
    <xsl:value-of select="PN:Middle/." />
    <xsl:value-of select="$space" />
    <xsl:value-of select="PN:Last/." />
    <!--<xsl:if test="PN:Pseudonim/.">
        <xsl:value-of select="$space" />
        (<xsl:value-of select="PN:Pseudonim/." />)
      </xsl:if>-->

  </xsl:template>

  <!-- 0009-000007 - ForeignCitizenNames -->
  <xsl:template name="ForeignCitizenNames">
    <xsl:variable name="hasCyrilicName">
      <xsl:value-of select="FCN:FirstCyrillic/."/>
    </xsl:variable>
    <xsl:if test="$hasCyrilicName!=''">
      <xsl:value-of select="FCN:FirstCyrillic/." />
      <xsl:value-of select="$space" />
      <xsl:value-of select="FCN:LastCyrillic/." />
      <xsl:value-of select="$space" />
      <xsl:value-of select="FCN:OtherCyrillic/." />
      <!--<xsl:if test="FCN:PseudonimCyrillic/.">
        <xsl:value-of select="$space" />
        <xsl:value-of select="FCN:PseudonimCyrillic/." />
      </xsl:if>-->
    </xsl:if>
    <xsl:variable name="hasLatinName">
      <xsl:value-of select="FCN:FirstLatin/."/>
    </xsl:variable>
    <xsl:if test="$hasCyrilicName!='' and  $hasLatinName!=''">
      (
    </xsl:if>
    <xsl:value-of select="FCN:FirstLatin/." />
    <xsl:value-of select="$space" />
    <xsl:value-of select="FCN:LastLatin/." />
    <xsl:value-of select="$space" />
    <xsl:value-of select="FCN:OtherLatin/." />
    <!--<xsl:if test="FCN:PseudonimCyrillic/.">
        <xsl:value-of select="$space" />
        <xsl:value-of select="FCN:PseudonimLatin/." />
      </xsl:if>-->
    <xsl:if test="$hasCyrilicName!='' and  $hasLatinName!=''">
      )
    </xsl:if>

  </xsl:template>

  <!-- 0009-000008 - PersonBasicData -->
  <xsl:template name="PersonBasicData">
    <xsl:for-each select="PBD:Names">
      <xsl:call-template name="PersonNames" />
    </xsl:for-each>
    <xsl:for-each select="PBD:Identifier">
      <xsl:call-template name="PersonIdentifier" />
    </xsl:for-each>
  </xsl:template>

  <!-- 0009-000011 - ForeignCitizenBasicData -->
  <xsl:template name="ForeignCitizenBasicData">
    <xsl:for-each select="FCBD:Names">
      <xsl:call-template name="ForeignCitizenNames" />
    </xsl:for-each>
    <xsl:value-of select="$space"/>
    <xsl:for-each select="FCBD:IdentityDocument">
      <!--Вид документ:-->
      ,<span class="dataLabel">
        <xsl:value-of select="FCID:DocumentType/." />
      </span>
      <!--Номер на документ:-->
      <xsl:value-of select="$space"/>
      <span class="dataLabel">
        <xsl:value-of select="FCID:DocumentNumber/." />
      </span>
      <xsl:value-of select="$space"/>

    </xsl:for-each>
    <xsl:for-each select="FCBD:BirthDate">
      , дата на раждане:
      <xsl:call-template name="FormatDate">
        <xsl:with-param name="DateTime" select="." />
      </xsl:call-template>
    </xsl:for-each>
    <xsl:for-each select="FCBD:PlaceOfBirth">
      , място на раждане:
      <span class="dataLabel">
        <!--<xsl:value-of select="FCPB:CountryCode/." />
        <xsl:value-of select="$space"/>-->
        <xsl:value-of select="FCPB:CountryName/." />,
        <xsl:value-of select="$space"/>
        <xsl:value-of select="FCPB:SettlementName/." />
      </span>
    </xsl:for-each>

  </xsl:template>

  <!-- 0009-000013 - EntityBasicData -->
  <xsl:template name="EntityBasicData">
    <span class="dataLabel uppercase">
      <xsl:value-of select="EBD:Name/."/>
    </span>
  </xsl:template>

  <xsl:template name="EntityBasicData2">
    <span class="dataLabel">
      <xsl:value-of select="EBD:Name/."/>
    </span>
    , ЕИК/БУЛСТАТ:
    <span class="dataLabel">
      <xsl:value-of select="EBD:Identifier/."/>
    </span>
  </xsl:template>

  <!-- 0009-000014 - ForeignEntityBasicData -->
  <xsl:template name="ForeignEntityBasicData">
    <!--Наименование на чуждестранно юридическо лице:-->
    <span class="dataLabel">
      <xsl:value-of select="FEBD:ForeignEntityName/."/>
    </span>
    <!--<xsl:value-of select="$space" />
    Код на държава:
    <span class="dataLabel">
      <xsl:value-of select="FEBD:CountryISO3166TwoLetterCode/."/>
    </span>-->
    <xsl:value-of select="$space" />
    Държава:
    <span class="dataLabel">
      <xsl:value-of select="FEBD:CountryNameCyrillic/."/>
    </span>
    <xsl:value-of select="$space" />
    <xsl:if test="FEBD:ForeignEntityRegister and FEBD:ForeignEntityIdentifier">
      Наименование на регистър в друга държава:
      <span class="dataLabel">
        <xsl:value-of select="FEBD:ForeignEntityRegister/."/>
      </span>
      <xsl:value-of select="$space" />
      Номер на регистър в друга държава:
      <span class="dataLabel">
        <xsl:value-of select="FEBD:ForeignEntityIdentifier/."/>
      </span>
      <xsl:value-of select="$space" />
    </xsl:if>
    <xsl:if test="FEBD:ForeignEntityOtherData">
      Други данни за чуждестранно юридическо лице:
      <span class="dataLabel">
        <xsl:value-of select="FEBD:ForeignEntityOtherData/."/>
      </span>

    </xsl:if>
  </xsl:template>

  <!-- 0009-000016 - ElectronicServiceApplicant -->
  <xsl:template name="ElectronicServiceApplicant">
    <xsl:for-each select="ESAp:RecipientGroup">
      <xsl:call-template name="RecipientGroup" />
      <br></br>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="Author">
    <xsl:if test="ESAu:Person">
      <xsl:for-each select="ESAu:Person">
        <xsl:call-template name="PersonBasicData" />
        <br></br>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="ESAu:ForeignCitizen">
      <xsl:for-each select="ESAu:ForeignCitizen">
        <xsl:call-template name="ForeignCitizenBasicData" />
      </xsl:for-each>
    </xsl:if>
  </xsl:template>

  <xsl:template name="Recipient">
    <xsl:if test="ESR:Person">
      <xsl:for-each select="ESR:Person">
        <xsl:call-template name="PersonBasicData" />
        <br></br>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="ESR:ForeignPerson">
      <xsl:for-each select="ESR:ForeignPerson">
        <xsl:call-template name="ForeignCitizenBasicData" />
        <br></br>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="ESR:Entity">
      <xsl:for-each select="ESR:Entity">
        <xsl:call-template name="EntityBasicData" />
        <br></br>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="ESR:ForeignEntity">
      <xsl:for-each select="ESR:ForeignEntity">
        <xsl:call-template name="ForeignEntityBasicData" />
        <br></br>
      </xsl:for-each>
    </xsl:if>
  </xsl:template>

  <xsl:template name="RecipientGroup">
    <xsl:for-each select="ESAp:Author">
      <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
      <xsl:call-template name="Author" />
    </xsl:for-each>
    <xsl:for-each select="ESAp:Recipient">
      <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
      <xsl:call-template name="Recipient" />
    </xsl:for-each>
  </xsl:template>

  <!-- 0009-000044 - RegisteredDocumentURI -->
  <xsl:template name="RegisteredDocumentURI">
    <xsl:if test="RDU:DocumentInInternalRegisterURI">
      <xsl:for-each select="RDU:DocumentInInternalRegisterURI">
        <span class="dataLabel">
          <xsl:value-of select="."/>
        </span>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="RDU:DocumentURI">
      <xsl:for-each select="RDU:DocumentURI">
        <xsl:call-template name="DocumentURI" />
      </xsl:for-each>
    </xsl:if>
  </xsl:template>

  <!-- 0009-000094 - PersonAddress -->
  <xsl:template name="PersonAddress">
    <span class="dataLabel">
      Обл.
      <xsl:value-of select="PA:DistrictGRAOName/." />
      <xsl:value-of select="$space" />
      Общ. <xsl:value-of select="PA:MunicipalityGRAOName/." />
      <xsl:value-of select="$space" />
      <xsl:value-of select="PA:SettlementGRAOName/." />
      <xsl:value-of select="$space" />
      ул. <xsl:value-of select="PA:StreetText/." />
      <xsl:value-of select="$space" />
      <xsl:value-of select="PA:BuildingNumber/." />
      вх. <xsl:value-of select="PA:Entrance/." /><xsl:value-of select="$space" />
      ет. <xsl:value-of select="PA:Floor/." /><xsl:value-of select="$space" />
      ап. <xsl:value-of select="PA:Apartment/." />
    </span>
  </xsl:template>

  <!-- Ukazaniq za otstranqvane na neredovnosti -->
  <xsl:template name ="Irregularities">
    <!--<table style="margin-left:20px;">
      <tr>
        <td colspan="2"></td>
      </tr>-->
    <ul>
    <xsl:for-each select="RII:Irregularities">
      <xsl:text></xsl:text>
      <li><xsl:value-of select="RII:IrregularityType/."/>
      <xsl:text> </xsl:text>
      <xsl:if test="RII:AdditionalInformationSpecifyingIrregularity/. != ''">
        &#160;&#160;&#160;&#160;&#160;<xsl:value-of select="RII:AdditionalInformationSpecifyingIrregularity/. " disable-output-escaping="yes" />
      </xsl:if>
      </li>
    </xsl:for-each>
    </ul>
    <!--</table>-->
  </xsl:template>

  <!-- Signature Templates -->

  <xsl:template name="Official">
    <xsl:param name="docNamespaceItem" />
    <xsl:variable name="space">
      <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
    </xsl:variable>
    <xsl:for-each select="*[local-name() = 'ForeignCitizenNames'][namespace-uri()=namespace-uri($docNamespaceItem)]">
      <xsl:value-of select="$space"></xsl:value-of>
      <xsl:call-template name="ForeignCitizenNames" />
    </xsl:for-each>
    <xsl:for-each select="*[local-name() = 'PersonNames'][namespace-uri()=namespace-uri($docNamespaceItem)]">
      <xsl:value-of select="$space"></xsl:value-of>
      <xsl:call-template name="PersonNames" />
    </xsl:for-each>
    <xsl:if test="*[local-name() = 'ElectronicDocumentAuthorQuality'][namespace-uri()=namespace-uri($docNamespaceItem)]/.">
      <br></br>
      <xsl:value-of select="$space"></xsl:value-of>В качеството на:
      <span style="padding-left:3px;">
        <xsl:value-of select="*[local-name() = 'ElectronicDocumentAuthorQuality'][namespace-uri()=namespace-uri($docNamespaceItem)]/."/>
      </span>
      <br></br>
    </xsl:if>
    <!--<xsl:variable name="SID" select="@SignatureUniqueID" />
    <xsl:if test="$SID">
      <xsl:for-each select="$SignatureXML/DocumentSignatures/Signature[@SignatureUniqueID=$SID]">
        <xsl:call-template name="DocumentSignature" />
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="not($SID)">
      <xsl:for-each select="$SignatureXML/DocumentSignatures/Signature">
        <xsl:call-template name="DocumentSignature" />
      </xsl:for-each>
    </xsl:if>-->
    <br></br>
  </xsl:template>

  <xsl:template name="TimeStampInfo">
    <table cellpadding="0" cellspacing="0" class="signTable">
      <tr>
        <td>
          Време на заверяване (TimeStamp):
          <span class="right" style="margin-left:3x;">
            <xsl:call-template name="FormatDateWithTime">
              <xsl:with-param name="DateTime" select="TimeStampTime/." />
            </xsl:call-template>
          </span>
        </td>
        <!--<td class="right">
        </td> -->
      </tr>
      <tr>
        <td colspan="1">
          <xsl:call-template name="SigningCertificateData" />
        </td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template name="SigningCertificateData">
    <table>
      <tr>
        <td class="mini header">
          Данни за сертификат
        </td>
      </tr>
      <tr>
        <td class="mini">
          Сериен номер:
          <span style="padding-left: 3px; padding-right:10px;">
            <xsl:value-of select="SigningCertificateData/SerialNumber/."/>
          </span>
          <br></br>
          Валиден от:  <span style="padding-left: 3px; padding-right:10px;" class="datalabel2">
            <xsl:call-template name="FormatDateWithTime">
              <xsl:with-param name="DateTime" select="SigningCertificateData/ValidFrom/." />
            </xsl:call-template>
          </span> Валиден до: <span style="padding-left: 3px;" class="datalabel2">
            <xsl:call-template name="FormatDateWithTime">
              <xsl:with-param name="DateTime" select="SigningCertificateData/ValidTo/." />
            </xsl:call-template>
          </span>
        </td>
      </tr>
      <tr>
        <td class="mini header">
          Информация за издателя
        </td>
      </tr>
      <tr>
        <td class="mini" style="text-align: left;">
          <xsl:value-of select="SigningCertificateData/Issuer/."/>
        </td>
      </tr>
      <tr>
        <td class="header mini" style="text-align: left;">
          Информация за автора и титуляра
        </td>
      </tr>
      <tr>
        <td class="mini" >
          <span style="text-align: left;">
            <xsl:value-of select="SigningCertificateData/Subject/."/>
          </span>
        </td>
      </tr>
      <tr>
        <td class="header mini">
          Информация за автора(доп.)
        </td>
      </tr>
      <tr>
        <td class="mini" style="text-align: left;">
          <xsl:value-of select="SigningCertificateData/SubjectAlternativeName/."/>
        </td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template name="DocumentSignature">
    <table cellpadding="0" cellspacing="0" class="signTable" style="margin:5px; width:140mm;">
      <tr>
        <td>
          Време на подписване:
          <span style="padding-left: 3px; padding-right:10px;">
            <xsl:call-template name="FormatDateOnly">
              <xsl:with-param name="DateTime" select="SignatureTime/." />
            </xsl:call-template>
          </span>
          Валидност на подписа: <span style="padding-left: 3px;">
            <xsl:choose>
              <xsl:when test="IsValid = 'true'">Да</xsl:when>
              <xsl:otherwise>Не</xsl:otherwise>
            </xsl:choose>
          </span>
        </td>
      </tr>
      <tr>
        <td>
          <xsl:call-template name="SigningCertificateData" />
        </td>
      </tr>
      <xsl:if test="count(child::TimeStampInfo/*) > 0">
        <tr>
          <td class="mini header"  nowrap="nowrap">
            Данни за време на заверяване (TimeStamp):
          </td>
        </tr>
      </xsl:if>
      <tr>
        <td style="padding-left:10px;">
          <xsl:if test="count(child::TimeStampInfo/*) > 0">
            <xsl:for-each select="TimeStampInfo">
              <xsl:call-template name="TimeStampInfo" />
            </xsl:for-each>
          </xsl:if>
        </td>
      </tr>
    </table>
  </xsl:template>

  <xsl:template name="LastSignDate">
    <xsl:variable name="SID" select="@SignatureUniqueID" />
    <!-- <xsl:for-each select="$SignatureXML/DocumentSignatures/Signature[@SignatureUniqueID=$SID]">
      <xsl:sort select="SignatureTime" order="descending" />
      <xsl:if test="position() = 1">
        <xsl:value-of select="SignatureTime/."/>
      </xsl:if>
    </xsl:for-each>-->

    <xsl:if test="$SID">
      <xsl:for-each select="$SignatureXML/DocumentSignatures/Signature[@SignatureUniqueID=$SID]">
        <xsl:sort select="SignatureTime" order="descending" />
        <xsl:if test="position() = 1">
          <xsl:call-template name="FormatDate">
            <xsl:with-param name="DateTime" select="SignatureTime/."></xsl:with-param>
          </xsl:call-template>
        </xsl:if>
      </xsl:for-each>
    </xsl:if>
    <xsl:if test="not($SID)">
      <xsl:for-each select="$SignatureXML/DocumentSignatures/Signature">
        <xsl:sort select="SignatureTime" order="descending" />
        <xsl:if test="position() = 1">
          <xsl:call-template name="FormatDate">
            <xsl:with-param name="DateTime" select="SignatureTime/."></xsl:with-param>
          </xsl:call-template>
        </xsl:if>
      </xsl:for-each>
    </xsl:if>
  </xsl:template>

  <!-- End Signature Templates -->

  <!-- R-2114 - InsurerData -->
  <xsl:template name="InsurerData">
    <span class="dataLabel">
      <xsl:value-of select="ID:InsurerName/." />
    </span>
    , БУЛСТАТ:
    <span class="dataLabel">
      <xsl:value-of select="ID:EntityIdentifier/." />
    </span>
  </xsl:template>

  <xsl:template name="InsurerData2">
    <table>
      <tr>
        <td>
          Наименование и адрес на осигурителя:
        </td>
      </tr>
      <tr>
        <td>
          <span class="dataLabel">
            <xsl:value-of select="ID:InsurerName/." />
          </span>,   БУЛСТАТ:
          <span class="dataLabel">
            <xsl:value-of select="ID:EntityIdentifier/." />
          </span>
        </td>
      </tr>
    </table>
  </xsl:template>

  <!-- 0009-000142 - Residence -->
  <xsl:template name="Residence">
    <tr>
      <td width="50%">
        oбл.  <span class="dataLabel">
          <xsl:value-of select="R:DistrictName/." />
        </span>
      </td>
      <td>
        <xsl:value-of select="$space"/>
        oбщ.
        <span class="dataLabel">
          <xsl:value-of select="R:MunicipalityName/." />
        </span>
      </td>
    </tr>
    <tr>
      <td width="50%">
        гр. /с./ <span class="dataLabel">
          <xsl:value-of select="R:SettlementName/." />
        </span>
        <xsl:value-of select="$space"/>
      </td>
      <td> п. к</td>
    </tr>
  </xsl:template>

  <!-- 0009-000172 - EntityManagementAddress -->
  <xsl:template name="EntityManagementAddress">
    <tr>
      <td colspan="2">
        кв. <span class="dataLabel">
          <xsl:value-of select="EMA:ResidentialComplex/." />
        </span>   <xsl:value-of select="$space"/> <xsl:value-of select="$space"/>
        Ул./бул./пл. <span class="dataLabel">
          <xsl:value-of select="EMA:StreetBoulevardSquare/." />
        </span> № <span class="dataLabel">
          <xsl:value-of select="EMA:ApartmentBuildingNumber/." />
        </span>
      </td>
    </tr>
    <tr>
      <td colspan="2">
        бл. № <span class="dataLabel">
          <xsl:value-of select="EMA:StreetBoulevardSquareNumber/." />
        </span>
        вх. <span class="dataLabel">
          <xsl:value-of select="EMA:Entrance/." />
        </span>
        ет. <span class="dataLabel">
          <xsl:value-of select="EMA:Floor/." />
        </span>
        ап. <span class="dataLabel">
          <xsl:value-of select="EMA:Apartment/." />
        </span>
      </td>
    </tr>
  </xsl:template>

<!-- 0009-000135 - CitizenshipRegistrationBasicData -->
  <xsl:template name="CitizenshipRegistrationBasicData">
    <xsl:for-each select="CRBD:PersonBasicData">
      <xsl:call-template name="PersonBasicData" />
    </xsl:for-each>
    , пол: <span class="dataLabel">
      <xsl:value-of select="CRBD:GenderName/." />
    </span>
    , дата на раждане: <span class="dataLabel">
      <xsl:value-of select="CRBD:BirthDate/." />
    </span>
    , място на раждане:
    <xsl:for-each select="CRBD:PlaceOfBirth">
      <xsl:call-template name="PlaceOfBirth"/>
    </xsl:for-each>
    <br></br>
    Гражданство:
    <xsl:for-each select="CRBD:Citizenships/CRBD:Citizenship">
      <span class="dataLabel">
        <xsl:value-of select="C:CountryName/." />
      </span>
      <br />
    </xsl:for-each>
  </xsl:template>

  <!-- 0009-000095 - PlaceOfBirth -->
  <xsl:template name="PlaceOfBirth">
    <span class="dataLabel">
      <xsl:value-of select="POB:DistrictGRAOName/." />
      <xsl:value-of select="$space"/>
      <xsl:value-of select="POB:MunicipalityGRAOName/." />
      <xsl:value-of select="$space"/>
      <xsl:value-of select="POB:SettlementGRAOName/." />
    </span>
  </xsl:template>

  <xsl:template name="PlaceOfBirthAbroad">
    <span class="dataLabel">
      <xsl:value-of select="POBА:CountryName /." />
      <xsl:value-of select="$space"/>
    </span>
    ,
    <xsl:value-of select="$space"/>
    гр. /с./   <span class="dataLabel">
      <xsl:value-of select="POBА:SettlementAbroadName /." />
    </span>
  </xsl:template>

  <xsl:template name="FormatDuration">
    <xsl:param name ="Duration"></xsl:param>
    <!--P nY nM nD T nH nM nS-->

    <xsl:value-of select="' '" />
    <xsl:call-template name="GetFormatDurationYear">
       <xsl:with-param name="Year" select="substring-before(substring-after($Duration, 'P'), 'Y')"></xsl:with-param>
    </xsl:call-template>
    
    <xsl:call-template name="GetFormatDurationMonth">
      <xsl:with-param name="Month" select="substring-before(substring-after($Duration, 'Y'), 'M')"></xsl:with-param>
    </xsl:call-template>
       
    <xsl:if test="substring-before(substring-after($Duration, 'M'), 'D') != ''">
      <xsl:call-template name="GetFormatDurationDay">
        <xsl:with-param name="Day" select="substring-before(substring-after($Duration, 'M'), 'D')"></xsl:with-param>
      </xsl:call-template>
    </xsl:if>
    
    <xsl:if test="substring-before(substring-after($Duration, 'P'), 'D') != ''">
      <xsl:call-template name="GetFormatDurationDay">
        <xsl:with-param name="Day" select="substring-before(substring-after($Duration, 'P'), 'D')"></xsl:with-param>
      </xsl:call-template>
    </xsl:if>
    
    <xsl:call-template name="GetFormatDurationTime">
     <xsl:with-param name="Time" select="substring-after($Duration, 'T')"></xsl:with-param>
    </xsl:call-template>
  </xsl:template>
  
  <xsl:template name="GetFormatDurationYear">
    <xsl:param name ="Year"></xsl:param>
    
     <xsl:if test="$Year != ''">
      <xsl:value-of select="$Year"/>
      <xsl:value-of select="' '" />
      <xsl:value-of select="'година/и'"/>
      <xsl:value-of select="' '" />
    </xsl:if>
  </xsl:template>  
  
  <xsl:template name="GetFormatDurationMonth">
    <xsl:param name ="Month"></xsl:param>   
     <xsl:if test="$Month != ''">
      <xsl:value-of select="$Month"/>
      <xsl:value-of select="' '" />
      <xsl:value-of select="'месец/и'"/>
      <xsl:value-of select="' '" />
    </xsl:if>
  </xsl:template>
  
  <xsl:template name="GetFormatDurationDay">
    <xsl:param name ="Day"></xsl:param>   
     <xsl:if test="$Day != ''">
      <xsl:value-of select="$Day"/>
      <xsl:value-of select="' '" />      
      <xsl:if test="$Day = '1'">
        <xsl:value-of select="'ден'"/>
      </xsl:if>      
      <xsl:if test="$Day != '1'">
        <xsl:value-of select="'дни'"/>
      </xsl:if>
      <xsl:value-of select="' '" />
    </xsl:if>
  </xsl:template>
  
  <xsl:template name="GetFormatDurationTime">
    <xsl:param name ="Time"></xsl:param>
      
     <xsl:if test="$Time != ''">
      <xsl:value-of select="substring-after($Time, 'H')"/>
      <xsl:value-of select="' '" />
      <xsl:value-of select="'час/а'"/>
      <xsl:value-of select="' '" />
     
      <xsl:if test="substring-before(substring-after($Time, 'H'), 'M') != ''">
        <xsl:value-of select="substring-before(substring-after($Time, 'H'), 'M')"/>
        <xsl:value-of select="' '" />
        <xsl:value-of select="'минути'"/>
        <xsl:value-of select="' '" />
      </xsl:if>
       
      <xsl:if test="substring-before($Time, 'M') != ''">
        <xsl:value-of select="substring-before($Time, 'M')"/>
        <xsl:value-of select="' '" />
        <xsl:value-of select="'минути'"/>
        <xsl:value-of select="' '" />
      </xsl:if>
       
      <xsl:if test="substring-before(substring-after($Time, 'M'), 'S') != ''">
        <xsl:value-of select="substring-before(substring-after($Time, 'M'), 'S')"/>
        <xsl:value-of select="' '" />
        <xsl:value-of select="'секунди'"/>
      </xsl:if>
      
      <xsl:if test="substring-before(substring-after($Time, 'H'), 'S') != ''">
        <xsl:value-of select="substring-before(substring-after($Time, 'H'), 'S')"/>
        <xsl:value-of select="' '" />
        <xsl:value-of select="'секунди'"/>
      </xsl:if>
  
      <xsl:if test="substring-before($Time, 'S') != ''">
        <xsl:value-of select="substring-before($Time, 'S')"/>
        <xsl:value-of select="' '" />
        <xsl:value-of select="'секунди'"/>
      </xsl:if>
      
    </xsl:if>
  </xsl:template>

<xsl:template name="GraoPersonBasicData">
    <table cellspacing="5">
      <xsl:for-each select="PBD:Names">
        <xsl:call-template name="GraoPersonNames" />
      </xsl:for-each>
      <xsl:for-each select="PBD:Identifier">
        <xsl:call-template name="GraoPersonIdentifier" />
      </xsl:for-each>
    </table>
  </xsl:template>
  <xsl:template name="GraoPersonNames">
    <tr>
      <td>
        Име
      </td>
      <td class="dottedLine" width="250px">
        <xsl:value-of select="PN:First/." />
      </td>
      <td class="dottedLine" width="250px">
        <xsl:value-of select="PN:Middle/." />
        <span class="invisible">
          <xsl:text>.</xsl:text>
        </span>
      </td>
      <td class="dottedLine" width="250px">
        <xsl:value-of select="PN:Last/." />
      </td>
    </tr>
    <tr>
      <td>
      </td>
      <td class="mini">
        собствено
      </td>
      <td class="mini">
        бащино
      </td>
      <td class="mini">
        фамилно
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="GraoPersonIdentifier">
    <xsl:if test="PI:EGN">
      <tr>
        <td>
          ЕГН:
        </td>
        <td class="dottedLine" width="250px">
          <xsl:value-of select="."/>
        </td>
        <td colspan="2">
        </td>
      </tr>
    </xsl:if>
    <xsl:if test="PI:LNCh">
      <tr>
        <td>
          ЛНЧ:
        </td>
        <td class="dottedLine" width="250px">
          <xsl:value-of select="."/>
        </td>
        <td colspan="2">
        </td>
      </tr>
    </xsl:if>
  </xsl:template>

  <xsl:template name="GRAOPersonAddress">
    <table>
      <tr>
        <td>с постоянен адрес:</td>
        <td class="dottedLine" width="250px">
          <xsl:value-of select="PA:DistrictGRAOName/." />
          <span class="invisible">
            <xsl:text>.</xsl:text>
          </span>
        </td>
        <td class="dottedLine" width="250px" colspan="8">
          <xsl:value-of select="PA:MunicipalityGRAOName/." />
          <span class="invisible">
            <xsl:text>.</xsl:text>
          </span>
        </td>
      </tr>
      <tr>
        <td></td>
        <td class="mini">област</td>
        <td class="mini" colspan="8">община</td>
      </tr>
      <tr>
        <td class="dottedLine" colspan="10">
          <xsl:value-of select="PA:SettlementGRAOName/." />
          <span class="invisible">
            <xsl:text>.</xsl:text>
          </span>
        </td>
      </tr>
      <tr>
        <td class="mini" colspan="10">
          населено място
        </td>
      </tr>
      <tr>
        <td colspan="2" class="dottedLine">
          <xsl:value-of select="PA:StreetText/." />
          <span class="invisible">
            <xsl:text>.</xsl:text>
          </span>
        </td>
        <td width="20px;"> №:</td>
        <td class="dottedLine">
          <xsl:value-of select="PA:BuildingNumber/." />
          <span class="invisible">
            <xsl:text>.</xsl:text>
          </span>
        </td>
        <td width="20px;"> вх.</td>
        <td class="dottedLine">
          <xsl:value-of select="PA:Entrance/." />
          <span class="invisible">
            <xsl:text>.</xsl:text>
          </span>
        </td>
        <td width="20px;">ет.</td>
        <td class="dottedLine">
          <xsl:value-of select="PA:Floor/." />
          <span class="invisible">
            <xsl:text>.</xsl:text>
          </span>
        </td>
        <td width="20px;">ап.</td>
        <td class="dottedLine">
          <xsl:value-of select="PA:Apartment/." />
          <span class="invisible">
            <xsl:text>.</xsl:text>
          </span>
        </td>
      </tr>
      <tr>
        <td class="mini" colspan="10">
          пл., бул., ул., ж.к., кв. и др.
        </td>
      </tr>
    </table>
  </xsl:template>

</xsl:stylesheet>