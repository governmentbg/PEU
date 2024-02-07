import * as React from "react";
import { IApplicationForInitialVehicleRegistrationDataContextProps, ApplicationForInitialVehicleRegistrationDataContext } from './ApplicationForInitialVehicleRegistrationDataUI';

export function withApplicationForInitialVehicleRegistrationDataContext<C extends React.ComponentClass<IApplicationForInitialVehicleRegistrationDataContextProps>>(Component: C): any {
    return function (props: any) {
        return (
            <ApplicationForInitialVehicleRegistrationDataContext.Consumer>
                {value =>
                    <Component {...props}
                        isOwnerOfVehicleRegistrationCouponMarked={value.isOwnerOfVehicleRegistrationCouponMarked}
                        isVehicleRepresentativeChoosen={value.isVehicleRepresentativeChoosen}
                        onChangeOwnerOfVehicleRegistrationCoupon={value.onChangeOwnerOfVehicleRegistrationCoupon}>
                        {props.children}
                    </Component>}
            </ApplicationForInitialVehicleRegistrationDataContext.Consumer>);
    }
}