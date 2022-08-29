<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:output method="html"/>
<xsl:template match="R">

<HTML>
     <HEAD> <TITLE>Test Report Small </TITLE> </HEAD>

    <BODY
          oncontextmenu="return false"
          background=".\TestReport.jpg"
          onload="if ( parent.adjustIFrameSize ) parent.adjustIFrameSize( window ) ;" >

        <!-- <H3>  -->
        <!--    <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>Test Report -->
        <!-- </H3> -->

   <!--     <TABLE border="0" > -->
               <TR>
                 <TD> <I> System Type:&#160;&#160; </I> </TD>
                    <TD> <B> <xsl:value-of select="ST/@TN"/> </B> </TD>
                    <BR></BR>
                   </TR>
             <TR>
                 <TD> <I> Date:&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </I> </TD>
                 <TD> <B> <xsl:value-of select="ST/@SD"/> </B> </TD>
                 <BR></BR>
                 </TR>
               <TR>
                  <TD> <I> Board Name:&#160;&#160; </I> </TD>
                  <TD> <B> <xsl:value-of select="ST/@NM"/> </B> </TD>
                <BR></BR>
               </TR>
                 <TR>
                  <TD>&#160;</TD>
              <TD>
                   <img src=".\program.jpg" />
               <BR></BR>
              </TD>
             </TR>

               <TR>
                    <TD> <I> Board Code:&#160;&#160;&#160; </I> </TD>
                    <TD> <B> <xsl:value-of select="ST/@BC"/> </B> </TD>
                    <BR></BR>
                        </TR>
                 <TR>
                  <TD>&#160;</TD>
              <TD>
               <img src=".\barcode.jpg" />
               <BR></BR>
              </TD>
             </TR>
               <TR>
                    <TD> <I> Operator:&#160;&#160;&#160;&#160;&#160;&#160;&#160; </I> </TD>
                    <TD> <B> <xsl:value-of select="ST/@OP"/> </B> </TD>
                          <BR></BR>
               </TR>
               <TR>
                 <TD> <I> Test Method:&#160;&#160; </I> </TD>
                    <TD> <B> <xsl:value-of select="ST/@TS"/> </B> </TD>
                         <BR></BR>
              </TR>
               <TR>
                    <TD> <I> Test Parameters: </I> </TD>
                  <TABLE border="0">
                         <xsl:choose>
                              <xsl:when test = "ST/@CI != '' and ST/@II != '' and ST/@IV != '' ">
                                   <TD>
                                        <TR>
                                             <TD>
                                                  <B> <xsl:value-of select="ST/@CI"/> </B>
                                             </TD>
                                        </TR>
                                        <TR>
                                             <TD>
                                                  <B> <xsl:value-of select="ST/@II"/> </B>
                                             </TD>
                                        </TR>
                                        <TR>
                                             <TD>
                                                  <B> <xsl:value-of select="ST/@IV"/> </B>
                                             </TD>
                                        </TR>
                                   </TD>
                              </xsl:when>
                              <xsl:otherwise>
                                   <TR>
                                        <TD> <xsl:text>&#160;&#160;</xsl:text> </TD>
                                        <TD> <xsl:value-of select="ST/@WS"/> </TD>
                                   </TR>
                              </xsl:otherwise>
                         </xsl:choose>
                    </TABLE>
               </TR>
     <!--     </TABLE> -->

<!--        <TABLE border="0" WIdTH="40%" >
               <TR>
                    <TD WIdTH="2%"/>
                    <TD> <xsl:value-of select="ST/@WS"/> </TD>
               </TR>
          </TABLE>           -->

          <xsl:choose>
               <xsl:when test="ST/@KR = 0 ">
                    Report Type: Only failed tested.
               </xsl:when>
               <xsl:when test="ST/@KR = 1 ">
                    Report Type: All component tested.
               </xsl:when>
             <xsl:otherwise/>
          </xsl:choose>

         <xsl:choose>
             <xsl:when test="ET/@NF != 0 and ET/@AK = 0 ">
                    <H4>
                         <xsl:text>&#160;</xsl:text> Total Test Time:  <xsl:text>&#160;</xsl:text>
                         <B> <xsl:value-of select="ET/@TT"/> </B>
                    </H4>
                    <font color="RED">
                              <H3> <xsl:text>&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text> Test FAIL </H3>
                    </font>
             </xsl:when>

              <xsl:when test="ET/@NT != 0 and ET/@AK = 0 " >
                    <H4>
                         <xsl:text>&#160;</xsl:text> Total Test Time:  <xsl:text>&#160;</xsl:text>
                         <B> <xsl:value-of select="ET/@TT"/> </B>
                    </H4>
                    <font color="BLUE">
                         <H3> <xsl:text>&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text> Test PASS </H3>
                    </font>
               </xsl:when>

               <xsl:otherwise>
                    <H3> <font color="RED"> <xsl:text>&#160;&#160;</xsl:text>
                         <xsl:choose>
                              <xsl:when test="ET/@AK = 0 ">
                              </xsl:when>
                              <xsl:when test="ET/@AK = 1 "> General Error. </xsl:when>
                              <xsl:when test="ET/@AK = 2 "> Abort by Operator. </xsl:when>
                              <xsl:when test="ET/@AK = 3 "> Markers Not Found. </xsl:when>
                              <xsl:when test="ET/@AK = 4 "> Z-Markers Not Found. </xsl:when>
                              <xsl:when test="ET/@AK = 5 "> COM Error. </xsl:when>
                              <xsl:when test="ET/@AK = 6 "> Data Base Error. </xsl:when>
                              <xsl:when test="ET/@AK = 7 "> Motion Error. </xsl:when>
                              <xsl:when test="ET/@AK = 8 "> Binary File Error. </xsl:when>
                              <xsl:when test="ET/@AK = 9 "> System ATE is OFF. </xsl:when>
                              <xsl:when test="ET/@AK = 10 "> System ATE is ON but can't initialize it. </xsl:when>
                              <xsl:when test="ET/@AK = 11 "> Test Motion Error. </xsl:when>
                              <xsl:when test="ET/@AK = 12 "> Too Many Tested Errors. </xsl:when>
                              <xsl:when test="ET/@AK = 13 "> Barcode Error. </xsl:when>
                              <xsl:otherwise> NOT Coded Error. </xsl:otherwise>
                         </xsl:choose>
                         <BR></BR>   <BR></BR>
                    </font>
                    <font color="BLACK">
                         <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text> Test Skipped !!!
                    </font>   </H3>
               </xsl:otherwise>
          </xsl:choose>

        <xsl:for-each select = "ET">
               <!-- <MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->
               <xsl:for-each select = "WARNING">
                    <TR>
                    <TD>
                              <font size="-1" color="TOMATO">
                                   <xsl:if test="@MaxTestedFailBoards != 0">
                                        <xsl:text>&#160; ( Warning Maximum Board Errors : &#160;</xsl:text>
                                        <B> <xsl:value-of select="@MaxTestedFailBoards"/> </B> <xsl:text>&#160;)</xsl:text>
                                        <BR></BR>
                                        <xsl:text>&#160; ( Warning Panel Not Complete Tested ) &#160;</xsl:text>
                                        <BR></BR>
                                   </xsl:if>
                              </font>
                         </TD>
                    </TR>
               </xsl:for-each>
               <!-- </MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->
          </xsl:for-each>

          <xsl:variable  name = "MaxPrtErr"  select = "ST/@ME"/>
        <!--   <xsl:value-of select="$MaxPrtErr" /> -->

        <xsl:for-each select = "BI">
             <TABLE>
                    <xsl:if test="@TR > 0 and @NF> 0">
                         <TR>
                         <TD>
                                   <font size="2" color="BLUE">
                                        Board: <xsl:text>&#160;</xsl:text> <B> <xsl:value-of select="@ID+1"/> </B>
                                   </font>
                              </TD>
                         <TD>
                                   <xsl:if test="@BC != ''">
                                        <font size="2" color="BLUE">
                                             Code: <xsl:text>&#160;</xsl:text> <B> <xsl:value-of select="@BC"/> </B>
                                        </font>
                                   </xsl:if>
                                   <xsl:if test="@BC = ''">
                                        <xsl:text>&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</xsl:text>
                                   </xsl:if>
                              </TD>
                         <TD>
                                   <font size="2" color="BLUE">
                                        Errors: <xsl:text>&#160;</xsl:text> <B> <xsl:value-of select="@NF"/> </B>
                                   </font>
                              </TD>
                         </TR>
						 <xsl:if test="@AK = 17 and @TR = 2">
							<TR><TD colspan="3">
							<xsl:text>&#160;</xsl:text>                    
								<FONT COLOR="red">
								Bad Mark found or board not present.
								</FONT>
							</TD></TR>   
						</xsl:if>
                    </xsl:if>
               </TABLE>

               <TABLE>
<!--           <TABLE width="100%"> -->
                    <xsl:if test="@TR > 0 and @NF> 0">
                         <xsl:for-each select = "TEST">
                              <xsl:variable  name = "PrintedErr"  select = "position()"/>
                              <!--   <xsl:value-of select="MaxPrtErr" />   -->
                              <!--   <xsl:value-of select="$PrintedErr" />   -->

                              <xsl:if test="$PrintedErr &lt;= $MaxPrtErr or $MaxPrtErr = 0">
                                   <TR>
                                        <TD>
<!--                                    <TD width="60%"> -->
                                             <font size="2">
                                                 <xsl:variable  name = "TestName"  select = "translate(@MK,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVXYZ')"/>
                                                 <xsl:variable  name = "Percentage"  select="substring-before(@MP,'%') "/>
						 <xsl:choose>
                                                       <xsl:when test=" $TestName =  '_CONTINUITY' or $TestName = 'CONTINUITY'or $TestName = 'CONTINUITY_BBT' ">
                                                            <xsl:text> OPEN:&#160;&#160;</xsl:text>
                                                            <xsl:value-of select="@C"/>
							    <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
                                                       </xsl:when>
                                                       <xsl:when test=" $TestName =  '_ISOLATION' or $TestName = 'ISOLATION' or $TestName = 'ISOLATION_BBT' ">
                                                            <xsl:text> SHORT:&#160;&#160;</xsl:text>
                                                            <xsl:value-of select="@C"/>
							    <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>                                                       </xsl:when>
                                                  <xsl:otherwise>
                                                            <xsl:text>&#160;&#160;</xsl:text> 
							    <xsl:value-of select="@NM"/>
							    <xsl:text>&#160;&#160;&#160;&#160;&#160;</xsl:text>
                                                    </xsl:otherwise>
                                                    </xsl:choose>
                                             </font>
                                        </TD>
	
							
  						
					
                                        <TD>
<!--                                    <TD width="40%"> -->
                                             <xsl:if test="@TR > 0">
                                                  <font size="2" color="RED">
                                                       <I> Fail:</I> <xsl:text>&#160;</xsl:text> <B> <xsl:value-of select='format-number($Percentage, "####.00")' />
							  % </B>
                                                  </font>
                                             </xsl:if>
                                             <xsl:if test="@TR = 0">
                                                  <font size="2" color="GREEN">
                                                       <I> Pass:</I> <xsl:text>&#160;</xsl:text> <B> <xsl:value-of select='format-number($Percentage, "####.00")'/>  
							%</B>
                                                  </font>
                                             </xsl:if>
                                             <xsl:if test="@TR = -1 ">
                                                  <font size="2" color="GRAY">
                                                       <I> Skip:</I> <xsl:text>&#160;</xsl:text> <B> <xsl:value-of select='format-number($Percentage, "####.00")'/> 
							% </B>
                                                  </font>
                                             </xsl:if>
                                        </TD>
                                   </TR>
                                             
                                   <xsl:if test="@FR != ''">
                                        <TR>
                                             <TD>
                                                  <PRE>
                                                       <xsl:text>&#160;&#160;</xsl:text>
                                                       <xsl:value-of disable-output-escaping="yes" select ="@FR" />
                                                  </PRE>
                                             </TD>
                                        </TR>
                                   </xsl:if>
                            </xsl:if>
                         </xsl:for-each>

                         <!-- <MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->
                         <xsl:for-each select = "WARNING">
                              <TR>
 <!--                             <TD>	-->
                                        <font size="-1" color="TOMATO">
                                             <xsl:if test="@MaxPrintedErrors != 0">
                                                  <xsl:text>&#160; ( Warning Maximum Printed Errors : &#160;</xsl:text>
                                                  <B> <xsl:value-of select="@MaxPrintedErrors"/> </B> <xsl:text>&#160;)</xsl:text>
                                             </xsl:if>
                                             <xsl:if test="@MaxWrittenErrors != 0">
                                                  <xsl:text>&#160; ( Warning Maximum Written Errors : &#160;</xsl:text>
                                                  <B> <xsl:value-of select="@MaxWrittenErrors"/> </B> <xsl:text>&#160;)</xsl:text>
                                             </xsl:if>
                                             <xsl:if test="@MaxTestedErrors != 0">
                                                  <xsl:text>&#160; ( Warning Maximum Tested Errors : &#160;</xsl:text>
                                                  <B> <xsl:value-of select="@MaxTestedErrors"/> </B> <xsl:text>&#160;)</xsl:text>
                                             </xsl:if>
                                        </font>
<!--                                   </TD>	-->
                              </TR>
                         </xsl:for-each>
                         <!-- </MLC Fri 20 Jan 2006> {E0398B1F-C0EC-4980-B46B-BE25239A0380} -->

                 </xsl:if>
               </TABLE>

          </xsl:for-each>

     </BODY>
</HTML>
</xsl:template>
</xsl:stylesheet>
