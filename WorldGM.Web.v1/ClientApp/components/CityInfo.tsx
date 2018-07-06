import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { DefaultComponent, City } from '../WorldGM';

export class CityInfo extends DefaultComponent<City> {

    constructor() {
        super();
    }

    public componentDidMount() {
        var teamId = this.props.match.params.id;
        fetch('api/city/' + teamId)
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({ values: data, loading: false });
            });
    }

    public render() {

        if (this.state.loading) {
            return <div><p><em>Loading...</em></p></div>
        }
        else {

            let city = this.state.values[0];
            let contents = <div>
                <h1>{city.name}</h1>
                <p>
                    {city.description}
                </p>
            </div>;

            return <div>{contents}</div>;
        }
    }

}