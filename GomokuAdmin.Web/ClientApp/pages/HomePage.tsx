import * as React from "react";
import { RouteComponentProps } from "react-router";
import { Helmet } from "react-helmet";

type Props = RouteComponentProps<{}>;

const HomePage: React.FC<Props> = () => {
    return <div>
        <Helmet>
            <title>Home page - GomokuAdmin.Web</title>
        </Helmet>
        <p className="text-center" style={{ "fontSize": "3rem" }}>Gomoku Admin page!</p>
    </div>;
}

export default HomePage;