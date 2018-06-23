import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { DefaultComponent, World, Continent, Region, City } from '../WorldGM';

export class WorldInfo extends DefaultComponent<World> {

    constructor() {
        super();
        
        fetch('api/World')
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({ values: data, loading: false });
            });
    }

    public render() {
        let world = this.state.values[0]; // We only expect 1
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            :  <div>
                <h1>{world.name}</h1>
                <p>
                    <em>Year {world.currentYear}</em>
                </p>

                <table className="table">
                    <thead>
                        <tr>
                            <th>City</th>
                            <th>Region</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            world.continents.map(continent =>
                                continent.regions.map(region =>
                                    region.cities.map(city =>
                                        <tr>
                                            <td>{city.name}</td>
                                            <td>{region.name}</td>
                                        </tr>
                                    )
                                )
                            )
                        
                    }
                    </tbody>
                </table>
            </div>;

        return <div>{contents}</div>;
    }

}


