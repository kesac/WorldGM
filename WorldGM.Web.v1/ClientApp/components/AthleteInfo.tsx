import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { DefaultComponent, AthleteViewModel } from '../WorldGM';

export class AthleteInfo extends DefaultComponent<AthleteViewModel> {

    constructor() {
        super();
    }

    public componentDidMount() {
        console.log(this.props.match.params.id);
        var athleteId = this.props.match.params.id;
        fetch('api/athlete/' + athleteId)
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({ values: data as AthleteViewModel[], loading: false });
            });
    }

    public render() {
        let athlete = this.state.values[0];

        let teamName = "Free Agent";
        let teamId = 0;

        if(athlete != null)
        {

            teamName = athlete.teamName;
            teamId = athlete.teamId;
            
        }
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <div>
                <h1>{athlete.name} <span className="text-muted"> | {athlete.position}</span></h1>

                <a href={'team/' + teamId}>{teamName}</a>

                <table className="table">
                    <thead>
                        <tr>
                            <th>Year</th>
                            <th>Age</th>
                            <th>OVR</th>
                            <th>OFF</th>
                            <th>DEF</th>
                            <th>PLAY</th>
                            <th>SHOT</th>
                            <th>PASS</th>
                            <th>ACC</th>
                            <th>BLK</th>
                            <th>STL</th>
                            <th>VIS</th>
                            <th>GK</th>
                            <th>SPD</th>
                            <th>END</th>
                        </tr>
                    </thead>
                    <tbody>
                    {
                        
                        <tr>
                            <td>218</td>
                            <td>{athlete.age}</td>
                            <td>{athlete.overallRating}</td>
                            <td>1</td>
                            <td>1</td>
                            <td>1</td>
                            <td>1</td>
                            <td>1</td>
                            <td>1</td>
                            <td>1</td>
                            <td>1</td>
                            <td>1</td>
                            <td>1</td>
                            <td>1</td>
                            <td>1</td>
                        </tr>
                        
                    }
                    </tbody>
                </table>
                
            </div>;

        return <div>{contents}</div>;
    }
    /**/

}


