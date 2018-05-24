import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

class City {
    name: string;
}

class Region {
    name: string;
    cities: City[];
}

class World {
    name: string;
    currentYear: number;
    regions: Region[];
}

interface WorldInfoState {
    worlds: World[];
    loading: boolean;
}


export class WorldInfo extends React.Component<RouteComponentProps<{}>, WorldInfoState> {
    constructor() {
        super();

        this.state = { worlds: [], loading: true };

        fetch('api/World')
            .then(response => {
                /*console.log(response);*/
                return response.json();
            })
            .then(data => {
                this.setState({ worlds: data as World[], loading: false });
            });
    }

    public render() {
        let world = this.state.worlds[0]; // We only expect 1
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
                        world.regions.map(region => 
                            region.cities.map(city => 
                                <tr>
                                    <td>{city.name}</td>
                                    <td>{region.name}</td>
                                </tr>
                            )
                        )
                    }
                    </tbody>
                </table>
            </div>;

        return <div>{contents}</div>;
    }
    /**/

}


