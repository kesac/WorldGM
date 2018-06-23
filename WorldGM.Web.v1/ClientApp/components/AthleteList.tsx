import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { DefaultComponent, AthleteViewModel } from '../WorldGM';

export class AthleteList extends DefaultComponent<AthleteViewModel> {

    constructor() {
        super();

        fetch('api/Athletes')
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({ values: data as AthleteViewModel[], loading: false });
            });
    }

    public render() {
        let athletes = this.state.values;
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            :  <div>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Position</th>
                            <th>Name</th>
                            <th>Age</th>
                            <th>Overall</th>
                        </tr>
                    </thead>
                    <tbody>
                    {
                        athletes.map(x => 
                                <tr>
                                    <td>{x.position}</td>
                                    <td><a href={'/athlete/' + x.id}>{x.name}</a></td>
                                    <td>{x.age}</td>
                                    <td>{x.overallRating}</td>
                                </tr>
                        )
                    }
                    </tbody>
                </table>
            </div>;

        return <div>{contents}</div>;
    }
    /**/

}


