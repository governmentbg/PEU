﻿import * as React from "react";
import { BaseProps } from "cnsys-ui-react";
import { EAUBaseComponent } from "eau-core";
import { ReportForChangingOwnershipOldOwnersDataVM, ReportForChangingOwnershipOldOwnersDataOldOwnersVM, PersonEntityChoiceType, PersonDataVM } from "../../models/ModelsAutoGenerated";
import { ReportForChangingOwnershipOwnerItemUI } from ".";
import { ArrayHelper } from "cnsys-core";
import { FieldFormUI } from "eau-documents";

export class ReportForChangingOwnershipOldOwnersDataUI extends EAUBaseComponent<BaseProps, ReportForChangingOwnershipOldOwnersDataVM>{
    constructor(props: BaseProps) {
        super(props);

    }


    render(): JSX.Element {
        return (
            <>
                {ArrayHelper.queryable.from(this.model.oldOwners).where(el => el.personEntityData.selectedChoiceType == PersonEntityChoiceType.Person).toArray().length > 0 ?
                    <FieldFormUI title={this.getResource("DOC_KAT_ReportForChangingOwnership_OwnersPerson_L")}>
                        <ul className="list-filed">
                            {ArrayHelper.queryable.from(this.model.oldOwners).where(el => el.personEntityData.selectedChoiceType == PersonEntityChoiceType.Person).toArray().map((item: ReportForChangingOwnershipOldOwnersDataOldOwnersVM, idx: number) => {
                                return (
                                    <li className="list-filed__item" role="group" key={idx}>
                                        <ReportForChangingOwnershipOwnerItemUI key={idx} {...this.bind(item.personEntityData, '')} />
                                    </li>
                                );
                            })}
                        </ul>
                    </FieldFormUI>
                    : null
                }
                {ArrayHelper.queryable.from(this.model.oldOwners).where(el => el.personEntityData.selectedChoiceType == PersonEntityChoiceType.Entity).toArray().length > 0 ?
                    <FieldFormUI title={this.getResource("DOC_KAT_ReportForChangingOwnership_OwnersEntity_L")}>
                        <ul className="list-filed" >
                            {ArrayHelper.queryable.from(this.model.oldOwners).where(el => el.personEntityData.selectedChoiceType == PersonEntityChoiceType.Entity).toArray().map((item: ReportForChangingOwnershipOldOwnersDataOldOwnersVM, idx: number) => {
                                return (
                                    <li className="list-filed__item" role="group" key={idx}>
                                        <ReportForChangingOwnershipOwnerItemUI key={idx} {...this.bind(item.personEntityData, '')} />
                                    </li>
                                );
                            })}
                        </ul>
                    </FieldFormUI>
                    : null
                }
            </>
        )
    }
}